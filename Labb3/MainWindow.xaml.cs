using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            string searchInput = SearchInput.Text;
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                var gymSessions = ((GymSessions)DataContext).AvailableSessions;
                AvailableSessionsList.ItemsSource = gymSessions;
                ResetSearchButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                GymSearch searcher = new GymSearch();
                var searchResult = searcher.SearchSessions(((GymSessions)DataContext).AvailableSessions, searchInput);

                AvailableSessionsList.ItemsSource = searchResult;
                ResetSearchButton.Visibility = searchResult.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void SearchInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search_Button(sender, e);
            }
        }

        public void ResetSearch_Button(object sender, RoutedEventArgs e)
        {
            SearchInput.Text = string.Empty;

            var gymSessions = ((GymSessions)DataContext).AvailableSessions;
            AvailableSessionsList.ItemsSource = gymSessions;
            ResetSearchButton.Visibility = Visibility.Collapsed;
        }

        private void Book_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var session = clickedButton.Tag as GymSessions.GymSession;
            var currentUser = new GymSessions.User { Name = Name };
            if (session != null)
            {
                if (!session.IsBooked)
                {
                    ((GymSessions)DataContext).BookSession(session, currentUser);

                    if (!session.IsFull)
                    {
                        MessageBox.Show($"Du har bokat {session.Name}-passet.");
                        clickedButton.IsEnabled = false;
                    }
                }
            }
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var session = clickedButton.Tag as GymSessions.GymSession;
            var currentUser = new GymSessions.User { Name = Name };

            if (session != null)
            {

                ((GymSessions)DataContext).CancelSession(session, currentUser);
                MessageBox.Show($"Du har avbokat {session.Name}-passet.");

                foreach (var item in AvailableSessionsList.Items)
                {
                    var container = AvailableSessionsList.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                    if (container != null)
                    {
                        var button = FindBookedButton<Button>(container);
                        if (button != null && button.Tag == session)
                        {
                            button.IsEnabled = true;
                        }
                    }
                }
            }
        }

        //ändra att det står bokad till boka på bokningsknappen
        private resetBooking FindBookedButton<resetBooking>(DependencyObject obj) where resetBooking : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is resetBooking)
                    return (resetBooking)child;
                else
                {
                    resetBooking childOfChild = FindBookedButton<resetBooking>(child);
                    if (childOfChild != null)
                    return childOfChild;
                }
            }
            return null;
        }


        //Om man klickar på ett gympass namn får man ytterligare info om det
        private void More_Info(object sender, MouseButtonEventArgs e)
        {
            TextBlock moreInfo = sender as TextBlock;
            var session = moreInfo?.Tag as GymSessions.GymSession;

            if (session != null)
            {
                string message = $"Passnamn: {session.Name}\n" +
                                 $"Kategori: {session.Category.Name}\n" +
                                 $"Starttid: {session.TimeOfDay:hh\\:mm}\n" +
                                 $"Längd: {session.Minutes} minuter\n" +
                                 $"Tillgängliga platser: {session.AvailableSlots}\n" +
                                 $"Vill du boka passet?";

                MessageBoxResult result = MessageBox.Show(message, "Passinformation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var currentUser = new GymSessions.User { Name = "David" };
                    if (!session.IsBooked)
                    {
                        ((GymSessions)DataContext).BookSession(session, currentUser);

                        if (!session.IsFull)
                        {
                            MessageBox.Show($"Du har bokat {session.Name}-passet.");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Det här passet är redan bokat.");
                    }
                }
            }
        }
    }
}
