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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
                GearList.ItemsSource = db.GearItems.Include(g=>g.Loaning);
                allLoadedItems = db.GearItems.Include(g => g.Loaning).Include(m => m.Loaning.Member).ToList();
            }
        }
        private void AddGearButton_Click(object sender, RoutedEventArgs e)
        {
            using(var db=new ApplicationDbContext())
            {
                using(var trx=db.Database.BeginTransaction())
                {
                    GearItem item = new GearItem()
                    {
                        Name=NameBox.Text,
                        Description=DescrBox.Text
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
    }
}

