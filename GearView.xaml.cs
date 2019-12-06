using ClimbingClub.Library;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            if(NameBox.Text.Length<1 || DescrBox.Text.Length<1 || NameBox.Text.Equals((Application.Current.Resources["Enter name"] as string)) || DescrBox.Text.Equals((Application.Current.Resources["Enter description"] as string)))
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Please set all fields and try again."] as string), (Application.Current.Resources["Error"] as string));
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
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Please set a positive number in number field."] as string), (Application.Current.Resources["Error"] as string));
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
            NameBox.Text = (Application.Current.Resources["Enter name"] as string);
            DescrBox.Text = (Application.Current.Resources["Enter description"] as string);
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

            if (SearchBox.Text.Equals((Application.Current.Resources["Search by name"] as string)))
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
                SearchBox.Text = (Application.Current.Resources["Search by name"] as string);
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Equals((Application.Current.Resources["Search by name"] as string)))
            {
                SearchBox.Text = "";
            }
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length < 1)
            {
                NameBox.Text = (Application.Current.Resources["Enter name"] as string);
            }
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Equals((Application.Current.Resources["Enter name"] as string)))
            {
                NameBox.Text = "";
            }
        }

        private void DescrBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DescrBox.Text.Equals((Application.Current.Resources["Enter description"] as string)))
            {
                DescrBox.Text = "";
            }
        }

        private void DescrBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DescrBox.Text.Length < 1)
            {
                DescrBox.Text = (Application.Current.Resources["Enter description"] as string);
            }
        }

        private void CountBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CountBox.Text.Length < 1)
            {
                CountBox.Text = (Application.Current.Resources["Enter number of gear items"] as string);
            }
        }

        private void CountBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountBox.Text.Equals((Application.Current.Resources["Enter number of gear items"] as string)))
            {
                CountBox.Text = "";
            }
        }

        private async void LoanStatusButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[0]).Text);
            int sum = 0;
            string result = (Application.Current.Resources["Items loaned to:"] as string);

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
                Text = (Application.Current.Resources["Items available"] as string) + " " + (Int32.Parse(((TextBlock)realSender.Children[3]).Text) - sum)+Environment.NewLine
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
                Title = Application.Current.Resources["Loan status"],
                Content = content,
                PrimaryButtonText = Application.Current.Resources["Confirm"] as string
            };

            descrDialog.ShowAsync();
        }
    }
}

