using ClimbingClub.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class MemberInfoView : Page
    {
        public int ID = 0;
        public Member memberInfo = null;
        public int monthlyFee = 0;
        public int oneTrainingFee = 0;
        public MemberInfoView()
        {
            this.InitializeComponent();
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            monthlyFee = Int32.Parse(localSettings.Values["monthly_fee"] as string);
            oneTrainingFee = Int32.Parse(localSettings.Values["one_training_fee"] as string);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MainActiveFrame.Navigate(typeof(MembersView));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ID = Int32.Parse((string)e.Parameter);
            using(var db = new ApplicationDbContext())
            {
                memberInfo = db.Members.Where(m => m.Id == ID).FirstOrDefault();
                MemberInfoBox.Text = memberInfo.Name + " " + memberInfo.Surname;
                PaymentList.ItemsSource = db.MembershipFees.Include(m => m.Member).Where(fee => fee.Member.Id == ID).OrderByDescending(fee => fee.Payment).ToList();
                var trainings = db.Trainings.Include(tr => tr.Member).Where(tr => tr.Member.Id == ID).ToList();
                foreach (Training tr in trainings)
                {
                    TrainingCalendar.SelectedDates.Add(new DateTime(tr.TrainingDate.Year, tr.TrainingDate.Month, tr.TrainingDate.Day));
                }
            }
            RefreshDebt();
        }

        private void RefreshDebt()
        {
            List<Training> debts = new List<Training>();
            using (var db = new ApplicationDbContext())
            {
                var trainings = db.Trainings.Include(tr => tr.Member).Where(tr => tr.Member.Id == ID).OrderByDescending(tr=>tr.TrainingDate).ToList();
                List<MembershipFee> payments = (List<MembershipFee>)PaymentList.ItemsSource;
                var paymentMonthList = payments.Where(fee => fee.IsMonthly);
                var paymentDaysList = payments.Where(fee => fee.IsMonthly==false);
                foreach (Training x in trainings)
                {
                    if(!(paymentMonthList.Where(pm=>pm.Payment.Month==x.TrainingDate.Month && pm.Payment.Year==x.TrainingDate.Year).Any() ||
                        paymentDaysList.Where(pm => pm.Payment.Date.Equals(x.TrainingDate.Date)).Any()))
                    {
                        debts.Add(x);
                    }
                }
            }
            DebtList.ItemsSource = debts;
            int[] unpaidTrainingsPerMonth = new int[12];
            foreach(var x in debts)
            {
                unpaidTrainingsPerMonth[x.TrainingDate.Month-1]++;
            }
            int sum = 0;
            for(int i=0;i<12;++i)
            {
                if(unpaidTrainingsPerMonth[i]>6)
                {
                    sum += monthlyFee;
                }
                else
                {
                    sum += unpaidTrainingsPerMonth[i] * oneTrainingFee;
                }
            }
            DebtSum.Text = "Member owns " +sum+ " KM";
        }

        private async void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock paymentTypeBlock = new TextBlock() { Text = "Select payment type" };
            TextBlock dateBlock = new TextBlock() { 
                Text = "Select date",
                Margin = new Thickness(0, 10, 0, 0)
            };
            ComboBox paymentCombo = new ComboBox()
            {
               ItemsSource=new List<string>() { "Monthly fee","One training fee"},
               SelectedIndex=0,
               Margin = new Thickness(0, 10, 0, 0)
            };
            DatePicker datePicker = new DatePicker()
            {
                Margin = new Thickness(0, 10, 0, 0),
                DayVisible=false,
                Date=DateTime.Now
            };
            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            content.Children.Add(paymentTypeBlock);
            content.Children.Add(paymentCombo);
            content.Children.Add(dateBlock);
            content.Children.Add(datePicker);
            paymentCombo.SelectionChanged += (senderarg,eventarg)=> PaymentTypeSelectionChanged(senderarg, eventarg,content,datePicker);

            ContentDialog paymentDialog = new ContentDialog()
            {
                Title = "Add payment",
                Content = content,
                PrimaryButtonText = "Confirm",
                SecondaryButtonText = "Cancel",
                IsSecondaryButtonEnabled = true
            };
            if (await paymentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                try
                {
                    AddPayment(datePicker, paymentCombo.SelectedIndex == 0);
                }
                catch (Exception ex)
                {
                    MessageDialog dialog = new MessageDialog("Value not set. Please enter a number.", "Error");
                    dialog.ShowAsync();
                }
            }
        }

        private void PaymentTypeSelectionChanged(object sender, SelectionChangedEventArgs e,StackPanel content,DatePicker datePicker)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
            if(selectedIndex==0)
            {
                datePicker.DayVisible = false;
            }
            else
            {
                datePicker.DayVisible = true;
            }
        }

        private void AddPayment(DatePicker datePicker,bool isMonthly)
        {
            DateTime dateToSet = new DateTime(datePicker.Date.Year,datePicker.Date.Month,datePicker.Date.Day);
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    using (var trx = db.Database.BeginTransaction())
                    {
                        if (db.MembershipFees.Include(fee => fee.Member).Where(fee => fee.Member.Id==ID &&
                        (isMonthly?(fee.IsMonthly && fee.Payment.Month==dateToSet.Month && fee.Payment.Year==dateToSet.Year):((fee.IsMonthly==false &&
                        fee.Payment.Date.Equals(dateToSet.Date)) || (fee.IsMonthly==true && 
                        fee.Payment.Month == dateToSet.Month && fee.Payment.Year == dateToSet.Year)))).Any())
                        {
                            MessageDialog messageDialog = new MessageDialog("You already paid that.", "Error");
                            messageDialog.ShowAsync();
                        }
                        else
                        {
                            MembershipFee newPayment = new MembershipFee()
                            {
                                Member = db.Members.Where(m=>m.Id==ID).First(),
                                IsMonthly = isMonthly,
                                Price = (isMonthly?monthlyFee:oneTrainingFee),
                                Payment = dateToSet
                            };
                            db.MembershipFees.Add(newPayment);
                            db.SaveChanges();
                        }
                        trx.Commit();
                        PaymentList.ItemsSource = db.MembershipFees.Include(m => m.Member).Where(fee => fee.Member.Id == ID).OrderByDescending(fee => fee.Payment).ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog("Something went wrong. Please try again.", "Error");
                messageDialog.ShowAsync();
            }
            RefreshDebt();
        }

        private async void LoaningButton_Click(object sender, RoutedEventArgs e)
        {

            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            TextBlock nameBlock = new TextBlock() { Text = "Enter name",
                Margin = new Thickness(0, 10, 0, 0)
            };
            TextBox nameBox = new TextBox() { Margin = new Thickness(0, 10, 0, 0) };
            TextBlock priceBlock = new TextBlock() { Text = "Enter price",
                Margin = new Thickness(0, 10, 0, 0)
            };
            TextBox priceBox = new TextBox() { Margin = new Thickness(0, 10, 0, 0) };
            TextBlock descrBlock = new TextBlock() { Text = "Enter description",
                Margin = new Thickness(0, 10, 0, 0)
            };
            TextBox descrBox = new TextBox() { Margin = new Thickness(0, 10, 0, 0) };
            TextBlock expectedRetDate = new TextBlock() { Text = "Expected return date",
                Margin=new Thickness(0,10,0,0)
            };
            DatePicker datePicker = new DatePicker()
            {
                Margin = new Thickness(0, 10, 0, 0),
                Date = DateTime.Now
            };
            TextBlock selectItemsBlock = new TextBlock()
            {
                Text = "Select items for loaning",
                Margin = new Thickness(0, 10, 0, 0)
            };
            ListView listGearItems = new ListView()
            {
                Margin = new Thickness(0, 10, 0, 0),
                Height=200
            };
            using (var db = new ApplicationDbContext())
            {
                foreach(var item in db.GearItems.Include(g=>g.Loaning))
                {
                    if (item.Loaning == null)
                    {
                        listGearItems.Items.Add(new CheckBox() { Content = item.Id + ", " + item.Name + ", " + item.Description });
                    }
                }
            }
            content.Children.Add(nameBlock);
            content.Children.Add(nameBox);
            content.Children.Add(priceBlock);
            content.Children.Add(priceBox);
            content.Children.Add(descrBlock);
            content.Children.Add(descrBox);
            content.Children.Add(expectedRetDate);
            content.Children.Add(datePicker);
            content.Children.Add(selectItemsBlock);
            content.Children.Add(listGearItems);
            double priceVar = 0.0;
            
            ContentDialog loaningDialog = new ContentDialog()
            {
                Title = "Add loaning",
                Content = content,
                PrimaryButtonText = "Confirm",
                SecondaryButtonText = "Cancel",
                IsSecondaryButtonEnabled = true
            };
            if (await loaningDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                try
                {
                    priceVar = Double.Parse(priceBox.Text);
                }
                catch (Exception ex)
                {
                    MessageDialog messageDialog = new MessageDialog("You entered an invalid price. Please try again.", "Error");
                    messageDialog.ShowAsync();
                    return;
                }
                Loaning loan = new Loaning()
                {
                    Name = nameBox.Text,
                    Price = priceVar,
                    Description = descrBox.Text,
                    LoanDate = DateTime.Now.Date,
                    ExpectedReturnDate = datePicker.Date.Date,
                    ReturnDate = DateTime.MinValue,
                };
                AddLoaning(loan, listGearItems);
            }
        }

        private void AddLoaning(Loaning loan,ListView listView)
        {
            using (var db = new ApplicationDbContext())
            {
                using (var trx = db.Database.BeginTransaction())
                {
                    Member membersLoan = db.Members.Where(m => m.Id == ID).FirstOrDefault();
                    loan.Member = membersLoan;
                    int count = 0;
                    foreach(var x in listView.Items)
                    {
                        if ((bool)((CheckBox)x).IsChecked)
                        {
                            int id = Int32.Parse(((CheckBox)x).Content.ToString().Split(',')[0]);
                            GearItem item = db.GearItems.Include(g => g.Loaning).Where(g => g.Id == id).FirstOrDefault();
                            item.Loaning = loan;
                            db.Update(item);
                            count++;
                        }
                    }
                    loan.Count = count;
                    db.Loanings.Add(loan);
                    db.SaveChanges();
                    trx.Commit();
                }
            }
        }
    }
}
