using ClimbingClub.Library;
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
using Microsoft.EntityFrameworkCore;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ClimbingClub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Frame MainActiveFrame;
        public MainPage()
        {
            this.InitializeComponent();
            MainActiveFrame = ActiveFrame;
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values["monthly_fee"]==null)
            {
                localSettings.Values["monthly_fee"] = "35";
            }
            if (localSettings.Values["one_training_fee"] == null)
            {
                localSettings.Values["one_training_fee"] = "5";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                /* Member pera = new Member() {
                     Level = 1,
                     Name = "Pera",
                     Surname="Peric"
                 };
                 db.Members.Add(pera);
                 Training trening = new Training()
                 {
                     TrainingDate = DateTime.Now,
                     Member = pera
                 };
                 db.Trainings.Add(trening);
                 db.SaveChanges();
                 System.Diagnostics.Debug.WriteLine("USPIO JE PERA!!!");

                 Loaning posudba = new Loaning()
                 {
                     Count=1,
                     Name="posudba",
                     Description="uzeo patike",
                     Member=db.Members.Where(m=>m.Id==1).FirstOrDefault(),
                     LoanDate=DateTime.Now,
                     ReturnDate=DateTime.Now,
                     ExpectedReturnDate=DateTime.Now
                 };
                 GearItem ajtem = new GearItem()
                 {
                     Loaning=posudba,
                     Description="tike broj 43",
                     Name="tikee"
                 };

                 db.Loanings.Add(posudba);
                 db.GearItems.Add(ajtem);

                 db.Loanings.RemoveRange(db.Loanings.Where(l => l.Count == 1));
                 db.SaveChanges();
                 db.MembershipFees.Add(new MembershipFee()
                 {
                     IsMonthly=true,
                     Price=35,
                     Payment=new DateTime(2019,6,25),
                     Member=db.Members.Where(m=>m.Id==1).FirstOrDefault()
                 });

                 db.MembershipFees.Add(new MembershipFee()
                 {
                     IsMonthly = false,
                     Price = 7,
                     Payment = DateTime.Now,
                     Member = db.Members.Where(m => m.Id == 1).FirstOrDefault()
                 });

                 db.SaveChanges();
                 db.Trainings.Add(new Training()
                {
                    TrainingDate=DateTime.Now,
                    Member= db.Members.Where(m => m.Id == 1).FirstOrDefault()
                });
                db.Trainings.Add(new Training()
                {
                    TrainingDate = new DateTime(2019, 10, 19),
                    Member = db.Members.Where(m => m.Id == 1).FirstOrDefault()
                });
                db.SaveChanges();
                 */


                //System.Diagnostics.Debug.WriteLine(db.GearItems.Where(gi => gi.Name.Equals("tikee")).FirstOrDefault());
                foreach (Training x in db.Trainings)
                {
                   // System.Diagnostics.Debug.WriteLine(x.TrainingDate +" "+ db.Trainings.Include(tr=>tr.Member).FirstOrDefault().Member.Name+"ASA");
                }
                foreach(Member x in db.Members)
                {
                    System.Diagnostics.Debug.WriteLine(x.Id+" "+x.Name+" "+x.Surname+" ");
                }
            }

            
        
        }


        private void rootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PivotTab.SelectedIndex==0)
            {
                ActiveFrame.Navigate(typeof(HomeView));
            }
            else if(PivotTab.SelectedIndex==1)
            {
                ActiveFrame.Navigate(typeof(MembersView));
            }
            else if(PivotTab.SelectedIndex==2)
            {
                ActiveFrame.Navigate(typeof(LoaningsView));
            }
            else
            {
                ActiveFrame.Navigate(typeof(GearView));
            }
        }

        private async void ChangeFees_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            TextBlock monthlyBlock = new TextBlock() { Text = "Enter monthly fee" };
            TextBlock oneTrainingBlock = new TextBlock() { Text = "Enter one training fee" };
            TextBox textBoxMonthly = new TextBox()
            {
                Height = 35,
                Text = localSettings.Values["monthly_fee"] as string
            };
            TextBox textBoxOneTraining = new TextBox()
            {
                Height = 35,
                Text = localSettings.Values["one_training_fee"] as string
            };
            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            content.Children.Add(monthlyBlock);
            content.Children.Add(textBoxMonthly);
            content.Children.Add(oneTrainingBlock);
            content.Children.Add(textBoxOneTraining);

            ContentDialog feesDialog = new ContentDialog()
            {
                Title = "Change fees",
                Content = content,
                PrimaryButtonText = "Confirm",
                SecondaryButtonText = "Cancel",
                IsSecondaryButtonEnabled = true
            };
            if (await feesDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                try
                {
                    Int32.Parse(textBoxOneTraining.Text);
                    Int32.Parse(textBoxMonthly.Text);
                    localSettings.Values["one_training_fee"] = textBoxOneTraining.Text;
                    localSettings.Values["monthly_fee"] = textBoxMonthly.Text;
                }
                catch(Exception ex)
                {
                    MessageDialog dialog = new MessageDialog("Value not set. Please enter a number.", "Error");
                    dialog.ShowAsync();
                }
            }
        }
    }
}
