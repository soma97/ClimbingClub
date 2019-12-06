using ClimbingClub.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                LoaningList.ItemsSource = db.Loanings.Include(l => l.Member).Include(l=>l.user);
                allLoaningsLoaded = db.Loanings.Include(l=>l.Member).Include(l => l.user).ToList();
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
                foreach (var x in db.GearLoanings.Where(gl=>gl.IdLoaning==id))
                {
                    GearItem item = db.GearItems.Where(gi => gi.Id == x.IdGearItem).FirstOrDefault();
                    listItems.Items.Add(item.Name + ", " + item.Description + ",  "+(Application.Current.Resources["Count"] as string) + ": " + x.CountLoaned);
                }
            }
            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            TextBlock textDescr = new TextBlock()
            {
                Text = description
            };
            TextBlock itemsBlock = new TextBlock()
            {
                Text = (Application.Current.Resources["Items"] as string)+":",
                Margin=new Thickness(0,10,0,0)
            };
     
            content.Children.Add(textDescr);
            content.Children.Add(itemsBlock);
            content.Children.Add(listItems);
            ContentDialog descrDialog = new ContentDialog()
            {
                Title = (Application.Current.Resources["Loaning description"] as string),
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
                    foreach(var x in db.GearLoanings.Where(l=>l.IdLoaning==id))
                    {
                        x.isActiveNow = false;
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

            if (SearchBox.Text.Equals((Application.Current.Resources["Search by loaner surname"] as string)))
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
                SearchBox.Text = (Application.Current.Resources["Search by loaner surname"] as string);
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Equals((Application.Current.Resources["Search by loaner surname"] as string)))
            {
                SearchBox.Text = "";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[0]).Text);
            using (var db = new ApplicationDbContext())
            {
                using (var trx = db.Database.BeginTransaction())
                {
                    Loaning loan = db.Loanings.Where(l => l.Id == id).FirstOrDefault();
                    db.Loanings.Remove(loan);
                    List<GearItemLoaning> gearItemsToDelete = new List<GearItemLoaning>();
                    foreach (var x in db.GearLoanings.Where(l => l.IdLoaning == id))
                    {
                        gearItemsToDelete.Add(x);
                    }
                    db.GearLoanings.RemoveRange(gearItemsToDelete);
                    db.SaveChanges();
                    trx.Commit();
                }
            }
            FillLoanList();
        }
    }
}

