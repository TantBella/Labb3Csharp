using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Labb3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GymModel();
        }
        private List<string> classes;


        public void Home_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hem-knappen klickades.");
        }

        public void ShowBookings_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Här ska man få se de pass man bokat.");
        }

        public void Search_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sök-knappen klickades.");
        }

        private void Book_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string sessionName = clickedButton.Content.ToString();
            MessageBox.Show($"Du har valt {sessionName}-passet.");
        }


        public class Users { }
    }
}
