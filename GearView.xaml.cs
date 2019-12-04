using ClimbingClub.Library;
using System;
using Microsoft.EntityFrameworkCore;
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
using Windows.UI.Popups;

namespace ClimbingClub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GearView : Page
    {
        public List<GearItem> allLoadedItems = null;
        public GearView()
        {
            this.InitializeComponent();
            FillGearList();
        }

        private void FillGearList()
        {
            using (var db = new ApplicationDbContext())
            {
                GearList.ItemsSource = db.GearItems;
                allLoadedItems = db.GearItems.ToList();
            }
        }
        private void AddGearButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameBox.Text.Length<1 || DescrBox.Text.Length<1 || NameBox.Text.Equals("Enter name") || DescrBox.Text.Equals("Enter description"))
            {
                MessageDialog messageDialog = new MessageDialog("Please set all fields and try again.", "Error");
                messageDialog.ShowAsync();
                return;
            }
            int number = 0;
            try
            {
                number = Int32.Parse(CountBox.Text);
                if(number<=0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }catch(Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog("Please set a positive number in number field.", "Error");
                messageDialog.ShowAsync();
                return;
            }
            using(var db=new ApplicationDbContext())
            {
                using(var trx=db.Database.BeginTransaction())
                {
                    GearItem item = new GearItem()
                    {
                        Name=NameBox.Text,
                        Description=DescrBox.Text,
                        CountAvailable=number
                    };
                    db.GearItems.Add(item);
                    db.SaveChanges();
                    trx.Commit();
                }
            }
            NameBox.Text = "Enter name";
            DescrBox.Text = "Enter description";
            FillGearList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[0]).Text);
            using(var db=new ApplicationDbContext())
            {
                using(var trx=db.Database.BeginTransaction())
                {
                    GearItem item = db.GearItems.Where(g => g.Id == id).FirstOrDefault();
                    db.GearItems.Remove(item);
                    db.SaveChanges();
                    trx.Commit();
                }
            }
            FillGearList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchBox.Text.Equals("Search by name"))
            {
                return;
            }
            if (SearchBox.Text.Length < 1)
            {
                FillGearList();
            }
            else
            {
                SearchByName(SearchBox.Text);
            }
        }

        private void SearchByName(string name)
        {
            GearList.ItemsSource = allLoadedItems.Where(g => g.Name.ToLower().StartsWith(name.ToLower()));
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Length < 1)
            {
                SearchBox.Text = "Search by name";
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Equals("Search by name"))
            {
                SearchBox.Text = "";
            }
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length < 1)
            {
                NameBox.Text = "Enter name";
            }
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Equals("Enter name"))
            {
                NameBox.Text = "";
            }
        }

        private void DescrBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DescrBox.Text.Equals("Enter description"))
            {
                DescrBox.Text = "";
            }
        }

        private void DescrBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DescrBox.Text.Length < 1)
            {
                DescrBox.Text = "Enter description";
            }
        }

        private void CountBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CountBox.Text.Length < 1)
            {
                CountBox.Text = "Enter number of gear items";
            }
        }

        private void CountBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountBox.Text.Equals("Enter number of gear items"))
            {
                CountBox.Text = "";
            }
        }

        private async void LoanStatusButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[0]).Text);
            int sum = 0;
            string result = "Items loaned to:";

            using (var db = new ApplicationDbContext())
            {
                foreach(var x in db.GearLoanings.Where(g => g.IdGearItem == id && g.isActiveNow==true))
                {
                    Member member = db.Loanings.Include(l => l.Member).Where(l => l.Id == x.IdLoaning).FirstOrDefault().Member;
                    result += Environment.NewLine + member.Name + " " + member.Surname + " (" + x.CountLoaned + ")";
                    sum += x.CountLoaned;
                }
            }
            TextBlock sumLoanedBlock = new TextBlock() {
                Text = "Items available: " + (Int32.Parse(((TextBlock)realSender.Children[3]).Text) - sum)+Environment.NewLine
            };
            TextBlock membersBlock = new TextBlock()
            {
                Text=result
            };
            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            content.Children.Add(sumLoanedBlock);
            if (sum > 0)
            {
                content.Children.Add(membersBlock);
            }

            ContentDialog descrDialog = new ContentDialog()
            {
                Title = "Loan status",
                Content = content,
                PrimaryButtonText = "Confirm"
            };

            descrDialog.ShowAsync();
        }
    }
}

