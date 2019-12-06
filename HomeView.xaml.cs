using ClimbingClub.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ClimbingClub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (MainPage.user==null)
            {
                AddUserButton.Visibility = Visibility.Collapsed;
                UsernameBox.IsEnabled = true;
                PasswordBox.IsEnabled = true;
                LoginButton.IsEnabled = true;
            }
            else
            {
                AddUserButton.Visibility = Visibility.Visible;
                UsernameBox.IsEnabled = false;
                PasswordBox.IsEnabled = false;
                LoginButton.IsEnabled = false;
            }
            AddUserButton.IsEnabled = MainPage.isAdmin;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using(var db=new ApplicationDbContext())
            {
                User user=db.Users.Where(u => u.Username.Equals(UsernameBox.Text)).FirstOrDefault();
                if (user!=null && user.Password.Equals(GetHash(PasswordBox.Password)))
                {
                    AddUserButton.Visibility = Visibility.Visible;
                    
                    AddUserButton.IsEnabled = MainPage.isAdmin = user.isAdmin;
                    MainPage.user = user.Username;

                    UsernameBox.IsEnabled = false;
                    PasswordBox.IsEnabled = false;
                    LoginButton.IsEnabled = false;
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Invalid username or password."] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                }
            }
        }

        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock usernameBlock = new TextBlock() { Text = (Application.Current.Resources["Enter username"] as string), Margin = new Thickness(0, 10, 0, 0) };
            TextBlock passwordBlock = new TextBlock() { Text = (Application.Current.Resources["Enter password"] as string), Margin = new Thickness(0, 10, 0, 0) };
            TextBox usernameBox = new TextBox() { Margin = new Thickness(0, 10, 0, 0) };
            PasswordBox passwordBox = new PasswordBox() { Margin = new Thickness(0, 10, 0, 0) };
            CheckBox adminCheck = new CheckBox() { Content = (Application.Current.Resources["Admin"] as string), Margin = new Thickness(0, 10, 0, 0) };
            StackPanel content = new StackPanel() { Orientation = Orientation.Vertical };
            content.Children.Add(usernameBlock);
            content.Children.Add(usernameBox);
            content.Children.Add(passwordBlock);
            content.Children.Add(passwordBox);
            content.Children.Add(adminCheck);

            ContentDialog userDialog = new ContentDialog()
            {
                Title = (Application.Current.Resources["Add user"] as string),
                Content = content,
                PrimaryButtonText = (Application.Current.Resources["Confirm"] as string),
                SecondaryButtonText = (Application.Current.Resources["Cancel"] as string),
                IsSecondaryButtonEnabled = true
            };
            if (await userDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if(usernameBox.Text.Length<1 || passwordBox.Password.Length < 1)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Please set all fields and try again."] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                using(var db = new ApplicationDbContext())
                {
                    if(db.Users.Where(u=>u.Username.Equals(usernameBox.Text)).Any())
                    {
                        MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Username already exists."] as string), (Application.Current.Resources["Error"] as string));
                        messageDialog.ShowAsync();
                        return;
                    }
                    db.Users.Add(new User() { Username = usernameBox.Text,
                        Password=GetHash(passwordBox.Password),
                        isAdmin=(bool)adminCheck.IsChecked
                    });
                    db.SaveChanges();
                }
            }
        }
        private string GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            byte[] byteResult = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            return Convert.ToBase64String(byteResult);
        }
    }
}
