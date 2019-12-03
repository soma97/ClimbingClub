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
using Windows.UI.Popups;
using Windows.UI;

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

        private void SurnameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text.Equals("Enter surname"))
            {
                SurnameBox.Text = "";
            }
        }

        private void SurnameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text.Length < 1)
            {
                SurnameBox.Text = "Enter surname";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length < 1 || SurnameBox.Text.Length < 1 || NameBox.Text.Equals("Enter name") || SurnameBox.Text.Equals("Enter surname"))
            {
                MessageDialog messageDialog = new MessageDialog("Please set all fields and try again.", "Error");
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
                        MessageDialog messageDialog = new MessageDialog("Invalid member data", "Error");
                        messageDialog.ShowAsync();
                    }
                }
            }
            NameBox.Text = "Enter name";
            SurnameBox.Text = "Enter surname";
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
            if (SearchBox.Text.Equals("Search by surname"))
            {
                SearchBox.Text = "";
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Length < 1)
            {
                SearchBox.Text = "Search by surname";
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
            if(SearchBox.Text.Equals("Search by surname"))
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
