using ClimbingClub.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Popups;

namespace ClimbingClub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MembersView : Page
    {
        public List<Member> allMembersLoaded = null;
        public MembersView()
        {
            this.InitializeComponent();
            RefreshMembersList();
        }

        private void RefreshMembersList()
        {
            using (var db = new ApplicationDbContext())
            {
                MemberList.ItemsSource = db.Members.Where(m=>m.isActive==true).ToList();
                allMembersLoaded = db.Members.Where(m => m.isActive == true).ToList();
            }
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length < 1)
            {
                NameBox.Text = (Application.Current.Resources["Enter personal name"] as string);
            }
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Equals((Application.Current.Resources["Enter personal name"] as string)))
            {
                NameBox.Text = "";
            }
        }

        private void SurnameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text.Equals((Application.Current.Resources["Enter surname"] as string)))
            {
                SurnameBox.Text = "";
            }
        }

        private void SurnameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text.Length < 1)
            {
                SurnameBox.Text = (Application.Current.Resources["Enter surname"] as string);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length < 1 || SurnameBox.Text.Length < 1 || NameBox.Text.Equals((Application.Current.Resources["Enter personal name"] as string)) || SurnameBox.Text.Equals((Application.Current.Resources["Enter surname"] as string)))
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Please set all fields and try again."] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
            using (var db=new ApplicationDbContext())
            {
                using(var trx=db.Database.BeginTransaction())
                {
                    try
                    {
                        Member newMember = new Member()
                        {
                            Name = NameBox.Text,
                            Surname = SurnameBox.Text,
                            isActive=true
                        };
                        db.Members.Add(newMember);
                        db.SaveChanges();
                        trx.Commit();
                    }
                    catch(Exception ex)
                    {
                        trx.Rollback();
                        MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Invalid member data."] as string), (Application.Current.Resources["Error"] as string));
                        messageDialog.ShowAsync();
                    }
                }
            }
            NameBox.Text = (Application.Current.Resources["Enter personal name"] as string);
            SurnameBox.Text = (Application.Current.Resources["Enter surname"] as string);
            RefreshMembersList();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((CheckBox)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[1]).Text);
            using (var db = new ApplicationDbContext())
            {
                using (var trx = db.Database.BeginTransaction())
                {
                    Member memberInfo = db.Members.Where(m => m.Id == id).FirstOrDefault();
                    if (!db.Trainings.Include(tr => tr.Member).Where(tr => tr.Member.Id == memberInfo.Id && tr.TrainingDate.Date.Equals(DateTime.Now.Date)).Any())
                    {
                        db.Trainings.Add(new Training()
                        {
                            Member = memberInfo,
                            TrainingDate = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                    trx.Commit();
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((CheckBox)sender).Parent;
            int id = Int32.Parse(((TextBlock)realSender.Children[1]).Text);
            using (var db = new ApplicationDbContext())
            {
                using (var trx = db.Database.BeginTransaction())
                {
                    Member memberInfo = db.Members.Where(m => m.Id == id).FirstOrDefault();
                    Training trainingRemove = db.Trainings.Include(tr => tr.Member).Where(tr => (tr.Member.Id == id && tr.TrainingDate.Date.Equals(DateTime.Now.Date))).FirstOrDefault();
                    db.Trainings.Remove(trainingRemove);
                    System.Diagnostics.Debug.WriteLine("Training removed: " + trainingRemove.TrainingDate+" " +trainingRemove.Member.Id);
                    db.SaveChanges();
                    trx.Commit();
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox realSender = (CheckBox)sender;
            if((bool)realSender.IsChecked)
            {
                CheckBox_Checked(realSender, e);
            }
            else
            {
                CheckBox_Unchecked(realSender, e);
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            MainPage.MainActiveFrame.Navigate(typeof(MemberInfoView), ((TextBlock)realSender.Children[1]).Text);
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Equals((Application.Current.Resources["Search by surname"] as string)))
            {
                SearchBox.Text = "";
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Length < 1)
            {
                SearchBox.Text = (Application.Current.Resources["Search by surname"] as string);
            }
        }

        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                using(var db=new ApplicationDbContext())
                {
                    MemberList.ItemsSource = db.Members.Where(m => m.Surname.StartsWith(SearchBox.Text));
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchBox.Text.Equals((Application.Current.Resources["Search by surname"] as string)))
            {
                return;
            }
            if(SearchBox.Text.Length<1)
            {
                RefreshMembersList();
            }
            else
            {
                SearchBySurname(SearchBox.Text);
            }
        }

        private void SearchBySurname(string surname)
        {
             MemberList.ItemsSource=allMembersLoaded.Where(m => m.Surname.ToLower().StartsWith(surname.ToLower()));
        }
    }
}
