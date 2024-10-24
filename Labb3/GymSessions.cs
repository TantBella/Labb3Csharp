using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Labb3
{
    public class GymSessions : INotifyPropertyChanged
    {
        private ObservableCollection<GymSession> _availableSessions;
        private ObservableCollection<GymSession> _bookedSessions;

        public ObservableCollection<GymSession> AvailableSessions
        {
            get { return _availableSessions; }
            set
            {
                _availableSessions = value;
                OnPropertyChanged("AvailableSessions");
            }
        }

        public ObservableCollection<GymSession> BookedSessions
        {
            get { return _bookedSessions; }
            set
            {
                _bookedSessions = value;
                OnPropertyChanged("BookedSessions");
            }
        }

        public User CurrentUser { get; set; } = new User { Name = "David" };

        public GymSessions()
        {


            var startTime = new[]
                    {
                new TimeSpan(9, 0, 0),
                new TimeSpan(9, 30, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(10, 30, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(11, 30, 0),
                new TimeSpan(12, 30, 0),
                new TimeSpan(13, 0, 0),
                new TimeSpan(14, 0, 0),
                new TimeSpan(14, 30, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 0, 0),
                new TimeSpan(16, 15, 0),
                new TimeSpan(16, 30, 0),
                new TimeSpan(16, 45, 0),
                new TimeSpan(17, 0, 0),
                new TimeSpan(17, 15, 0),
                new TimeSpan(17, 30, 0),
                new TimeSpan(17, 0, 0),
                new TimeSpan(18, 0, 0),
                new TimeSpan(18, 30, 0),
                new TimeSpan(18, 45, 0),
                new TimeSpan(19, 0, 0),
                new TimeSpan(19, 30, 0),
                new TimeSpan(20, 0, 0),
                new TimeSpan(20, 30, 0),
            };

            var categories = new[]
            {
                new Category("Kondition"),
                new Category("Grupppass"),
                new Category("Stretch"),
                new Category("Styrka")
            };

            int BookedSlots = 10;

            AvailableSessions = new ObservableCollection<GymSession>
            {
                new GymSession { Id = 1, Name = "PT", TimeOfDay = startTime[0], Minutes = 60, Category = categories[3], AvailableSlots = 10  - BookedSlots},
                new GymSession { Id = 2, Name = "Yoga", TimeOfDay = startTime[1], Minutes = 50, Category = categories[2], AvailableSlots = 12 - BookedSlots},
                new GymSession { Id = 3, Name = "Core", TimeOfDay = startTime[2], Minutes = 60, Category = categories[1], AvailableSlots = 15 - BookedSlots },
                new GymSession { Id = 4, Name = "Spinning", TimeOfDay = startTime[3], Minutes = 45, Category = categories[0], AvailableSlots = 20 - BookedSlots}
            };

            BookedSessions = new ObservableCollection<GymSession>();
        }

        public void BookSession(GymSession session, User user)
        {
            if (!session.BookSlot())
            {

                MessageBox.Show("Passet är fullbokat.");
           
            }
            else if (!session.IsBooked || session.BookSlot())
            {
                session.AddParticipant(user);
                session.IsBooked = true; 
                BookedSessions.Add(session);
                OnPropertyChanged("BookedSessions");
            }
            //else
            //{
            //    session.AddParticipant(user);
            //    BookedSessions.Add(session);
            //    OnPropertyChanged("BookedSessions");
            //}
        }

        public void CancelSession(GymSession session, User user)
        {
            session.CancelSlot();
            session.RemoveParticipant(user);
            session.IsBooked = false;
            BookedSessions.Remove(session);
            OnPropertyChanged("BookedSessions");


            OnPropertyChanged("AvailableSessions");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class GymSession : INotifyPropertyChanged
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Category Category { get; set; }
            public TimeSpan TimeOfDay { get; set; }
            public int Minutes { get; set; }
            public List<User> Participants { get; set; } = new List<User>();

            private int _availableSlots;
            public int AvailableSlots
            {
                get { return _availableSlots; }
                set
                {
                    _availableSlots = value;
                    OnPropertyChanged("AvailableSlots");
                }
            }

            int BookedSlots { get; set; }
            public bool IsFull => AvailableSlots <= 0;

            private bool _isBooked;
            public bool IsBooked
            {
                get { return _isBooked; }
                set
                {
                    _isBooked = value;
                    OnPropertyChanged("IsBooked");
                }
            }

            public bool BookSlot()
            {
                if (!IsFull)
                {
                    AvailableSlots--;
                    return true;
                }
                return false;
            }

            public void CancelSlot()
            {
                if (BookedSlots > 0)
                {
                    AvailableSlots++;
                }
            }

            public void AddParticipant(User user)
            {
                Participants.Add(user);
                IsBooked = true;
                OnPropertyChanged("Participants");
            }

            public void RemoveParticipant(User user)
            {
                Participants.Remove(user);
                if (Participants.Count == 0)
                {
                    IsBooked = false;
                }
                OnPropertyChanged("Participants");
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class User
        {
            public string Name { get; set; }
        }

        public class Category
        {
            public string Name { get; set; }

            public Category(string name)
            {
                Name = name;
            }
        }
    }
}
