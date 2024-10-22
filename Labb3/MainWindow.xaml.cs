using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Labb3.GymSessions;

namespace Labb3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                DataContext = new GymSessions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Något gick fel: {ex.Message}");
            }
        }

        public void ShowBookings_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            AvailableSessionsList.Visibility = AvailableSessionsList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            BookedSessionsList.Visibility = BookedSessionsList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            if (AvailableSessionsList.Visibility == Visibility.Visible)
            {
                clickedButton.Content = "Mina pass";
                UserName.Visibility = Visibility.Collapsed;
            }
            else
            {
                clickedButton.Content = "Visa alla pass";
                UserName.Visibility = Visibility.Visible; 
            }
        }


        public void Search_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sök-knappen klickades.");
        }

        private void Book_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var session = clickedButton.Tag as GymSessions.GymSession;
            var currentUser = new GymSessions.User { Name = "David" };
            if (session != null)
            {
                if (!session.IsBooked)
                {
                    // Try to book the session
                    ((GymSessions)DataContext).BookSession(session, currentUser);

                    if (!session.IsFull)
                    {
                        MessageBox.Show($"Du har bokat {session.Name}-passet.");
                        clickedButton.IsEnabled = false;
                        clickedButton.Content = "Bokad";
                    }
                }
            }
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var session = clickedButton.Tag as GymSessions.GymSession;
            var currentUser = new GymSessions.User { Name = "David" }; 
            if (session != null)
            {
                ((GymSessions)DataContext).CancelSession(session, currentUser);
                MessageBox.Show($"Du har avbokat {session.Name}-passet.");
            }
        }

    }
}
