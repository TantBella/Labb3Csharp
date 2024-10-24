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
                //0
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
                //10
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
                //20
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
                new GymSession { Id = 1, Name = "PT", TimeOfDay = startTime[0], Minutes = 60, Category = categories[3], AvailableSlots = 1  - BookedSlots},
                new GymSession { Id = 2, Name = "Lätt Yoga", TimeOfDay = startTime[1], Minutes = 50, Category = categories[2], AvailableSlots = 12 - BookedSlots},
                new GymSession { Id = 3, Name = "Core", TimeOfDay = startTime[2], Minutes = 60, Category = categories[1], AvailableSlots = 15 - BookedSlots },
                new GymSession { Id = 4, Name = "Spinning", TimeOfDay = startTime[3], Minutes = 45, Category = categories[0], AvailableSlots = 20 - BookedSlots},
                new GymSession { Id = 5, Name = "Lunch-Yoga", TimeOfDay = startTime[4], Minutes = 30, Category = categories[0], AvailableSlots = 15 - BookedSlots},
                new GymSession { Id = 6, Name = "Lunch-Bodypump", TimeOfDay = startTime[4], Minutes = 30, Category = categories[1], AvailableSlots = 11 - BookedSlots},
                new GymSession { Id = 7, Name = "Spinning", TimeOfDay = startTime[13], Minutes = 45, Category = categories[0], AvailableSlots = 20 - BookedSlots},
                new GymSession { Id = 8, Name = "Core", TimeOfDay = startTime[14], Minutes = 60, Category = categories[1], AvailableSlots = 10 - BookedSlots},
                new GymSession { Id = 9, Name = "Step", TimeOfDay = startTime[16], Minutes = 45, Category = categories[1], AvailableSlots = 15 - BookedSlots},
                new GymSession { Id = 10, Name = "Spinning", TimeOfDay = startTime[19], Minutes = 45, Category = categories[0], AvailableSlots = 10 - BookedSlots},
                new GymSession { Id = 11, Name = "PT", TimeOfDay = startTime[20], Minutes = 90, Category = categories[3], AvailableSlots = 1 - BookedSlots},
                new GymSession { Id = 12, Name = "Spinning, intensiv", TimeOfDay = startTime[20], Minutes = 45, Category = categories[0], AvailableSlots = 20 - BookedSlots},
                new GymSession { Id = 13, Name = "Yoga", TimeOfDay = startTime[23], Minutes = 50, Category = categories[2], AvailableSlots = 15 - BookedSlots},
                new GymSession { Id = 14, Name = "Gympa", TimeOfDay = startTime[25], Minutes = 60, Category = categories[1], AvailableSlots = 25 - BookedSlots},
                new GymSession { Id = 15, Name = "Skivstång -Lätt", TimeOfDay = startTime[25], Minutes = 45, Category = categories[3], AvailableSlots = 20 - BookedSlots},
                new GymSession { Id = 15, Name = "Skivstång", TimeOfDay = startTime[26], Minutes = 45, Category = categories[3], AvailableSlots = 20 - BookedSlots}
            };

            BookedSessions = new ObservableCollection<GymSession>();
        }

        public void BookSession(GymSession session, User user)
        {
            if (session.BookSlot())
            {
                session.AddParticipant(user);
                BookedSessions.Add(session);
                OnPropertyChanged("BookedSessions");
            }
        }

        public void CancelSession(GymSession session, User user)
        {
            session.RemoveParticipant(user);
            session.CancelSlot();
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

            public string ButtonContent
            {
                get
                {
                    return IsFull ? "Fullbokad" : IsBooked ? "Bokad" : "Boka pass";
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
                    OnPropertyChanged("ButtonContent");
                }
            }

            public bool BookSlot()
            {
                if (!IsFull)
                {
                    AvailableSlots--;
                    OnPropertyChanged("ButtonContent");
                    return true;
                }
                return false;
            }

            public void CancelSlot()
            {
                    AvailableSlots++;
                    OnPropertyChanged("ButtonContent");
                
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
