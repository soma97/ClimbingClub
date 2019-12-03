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

            TextBlock monthlyBlock = new TextBlock() { Text = "Enter monthly fee",
                Margin=new Thickness(0,10,0,0)
            };
            TextBlock oneTrainingBlock = new TextBlock() { Text = "Enter one training fee",
                Margin = new Thickness(0, 10, 0, 0)
            };
            TextBox textBoxMonthly = new TextBox()
            {
                Height = 35,
                Text = localSettings.Values["monthly_fee"] as string,
                Margin = new Thickness(0, 10, 0, 0)
            };
            TextBox textBoxOneTraining = new TextBox()
            {
                Height = 35,
                Text = localSettings.Values["one_training_fee"] as string,
                Margin = new Thickness(0, 10, 0, 0)
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
