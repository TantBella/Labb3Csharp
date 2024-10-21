using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static Labb3.MainWindow;

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

        public GymSessions()
        {
            var startTime = new[]
            {
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 30, 0),
                new TimeSpan(14, 30, 0),
                new TimeSpan(19, 0, 0)
            };

            AvailableSessions = new ObservableCollection<GymSession>
            {
                new GymSession { Id = 1, Name = "PT", TimeOfDay = startTime[0], Minutes = 60 },
                new GymSession { Id = 2, Name = "Yoga", TimeOfDay = startTime[1], Minutes = 50 },
                new GymSession { Id = 3, Name = "Core", TimeOfDay = startTime[2], Minutes = 60 },
                new GymSession { Id = 4, Name = "Spinning", TimeOfDay = startTime[3], Minutes = 45 }
            };

            BookedSessions = new ObservableCollection<GymSession>();
        }

        public void BookSession(GymSession session, User user)
        {
            session.AddParticipant(user);
            AvailableSessions.Remove(session);
            BookedSessions.Add(session);
        }

        public void CancelSession(GymSession session, User user)
        {
            session.RemoveParticipant(user); 
            BookedSessions.Remove(session);
            AvailableSessions.Add(session);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       

        public class GymSession
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public TimeSpan TimeOfDay { get; set; }
            public int Minutes { get; set; }
            public List<User> Participants { get; set; } = new List<User>();

            public void AddParticipant(User user)
            {
                Participants.Add(user);
            }

            public void RemoveParticipant(User user)
            {
                Participants.Remove(user);
            }
        }

        public class User
        {
            public string Name { get; set; }

            public void BookSession(GymSession session)
            {
                session.AddParticipant(this);
            }

            public void CancelBooking(GymSession session)
            {
                session.RemoveParticipant(this);
            }
        }
        
    }
}

