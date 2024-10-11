using System.Collections.ObjectModel;
using System.ComponentModel;

public class GymModel : INotifyPropertyChanged
{
    private ObservableCollection<TrainingSession> _availableSessions;

    public ObservableCollection<TrainingSession> AvailableSessions
    {
        get { return _availableSessions; }
        set
        {
            _availableSessions = value;
            OnPropertyChanged("AvailableSessions");
        }
    }

    public GymModel()
    {
        var startTime = new[]
       {
            new TimeSpan(9, 0, 0), 
            new TimeSpan(10, 30, 0), 
            new TimeSpan(14, 30, 0), 
            new TimeSpan(19, 0, 0)   
        };

        AvailableSessions = new ObservableCollection<TrainingSession>
        {
    new TrainingSession { Id = 1, Name = "PT", TimeOfDay = startTime[0], Minutes = 60 },
            new TrainingSession { Id = 2, Name = "Yoga", TimeOfDay = startTime[1], Minutes = 50 }, 
            new TrainingSession { Id = 3, Name = "Core", TimeOfDay = startTime[2], Minutes = 60 },
            new TrainingSession { Id = 4, Name = "Spinning", TimeOfDay = startTime[3], Minutes = 45 }
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class TrainingSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public int Minutes { get; set; }
    }
}
