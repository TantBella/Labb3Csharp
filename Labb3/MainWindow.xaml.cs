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
            //MessageBox.Show("Här ska man få se de pass man bokat.");
            AvailableSessionsList.Visibility = AvailableSessionsList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            BookedSessionsList.Visibility = BookedSessionsList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
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
                    ((GymSessions)DataContext).BookSession(session, currentUser);
                    session.IsBooked = true;
                    MessageBox.Show($"Du har bokat {session.Name}-passet.");
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
