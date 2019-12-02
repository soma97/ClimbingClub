using ClimbingClub.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ClimbingClub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoaningsView : Page
    {
        public List<Loaning> allLoaningsLoaded = null;
        public LoaningsView()
        {
            this.InitializeComponent();
            FillLoanList();
        }

        private void FillLoanList()
        {
            using (var db = new ApplicationDbContext())
            {
                LoaningList.ItemsSource = db.Loanings.Include(l => l.Member);
                allLoaningsLoaded = db.Loanings.Include(l=>l.Member).ToList();
            }
        }
        private async void DescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            string description = "";
            int id = Int32.Parse(((TextBlock)realSender.Children[0]).Text);
            ListView listItems = new ListView()
            {
                Height=250
            };
            using (var db = new ApplicationDbContext())
            {
                description = db.Loanings.Where(l => l.Id == id).Select(l => l.Description).FirstOrDefault();
                foreach (var x in db.GearItems.Include(g => g.Loaning).Where(l => l.Loaning.Id == id))
                {
                    listItems.Items.Add(x.Id + ", "+x.Name + ", " + x.Description);
                }
            }
            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            TextBlock textDescr = new TextBlock()
            {
                Text = description
            };
            TextBlock itemsBlock = new TextBlock()
            {
                Text = "Items:",
                Margin=new Thickness(0,10,0,0)
            };
     
            content.Children.Add(textDescr);
            content.Children.Add(itemsBlock);
            content.Children.Add(listItems);
            ContentDialog descrDialog = new ContentDialog()
            {
                Title = "Loaning description",
                Content = content,
                PrimaryButtonText = "OK"
            };
            await descrDialog.ShowAsync();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[0]).Text);
            using (var db = new ApplicationDbContext())
            {
                using (var trx = db.Database.BeginTransaction())
                {
                    Loaning loan = db.Loanings.Where(l => l.Id == id).FirstOrDefault();
                    loan.ReturnDate = DateTime.Now;
                    db.Entry(loan).Property("ReturnDate").IsModified = true;
                    foreach(var x in db.GearItems.Include(g=>g.Loaning).Where(l=>l.Loaning.Id==id))
                    {
                        x.Loaning = null;
                        db.Update(x);
                    }
                    db.SaveChanges();
                    trx.Commit();
                }
            }
            FillLoanList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchBox.Text.Equals("Search by loaner surname"))
            {
                return;
            }
            if (SearchBox.Text.Length < 1)
            {
                FillLoanList();
            }
            else
            {
                SearchBySurname(SearchBox.Text);
            }
        }

        private void SearchBySurname(string surname)
        {
            LoaningList.ItemsSource = allLoaningsLoaded.Where(l => l.Member.Surname.ToLower().StartsWith(surname.ToLower()));
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Length < 1)
            {
                SearchBox.Text = "Search by loaner surname";
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Equals("Search by loaner surname"))
            {
                SearchBox.Text = "";
            }
        }
    }
}

