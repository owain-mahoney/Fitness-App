using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using fitnessApp.models;
using System.IO;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Diagnostics;

namespace fitnessApp.viewModel
{
   public class ProgressPageViewModel : INotifyPropertyChanged
    {
        Stopwatch stopwatch;
        public string timer = "00:00:00";
        private readonly exceriseDBOps exceriseDBOps;
        private SQLiteAsyncConnection _connection;
        private Databasemodel _dbModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool inProgress = false;
        
        private double distance = 0;
        private double speed = 0;
        private double cal = 0;
        
        private bool endVisable = false;
        private bool pauseVisable = false;
        private bool startVisable = true;

        private geolocation GPS;
        private Tables.Point currentPoint;

        double MetValue = 1; 

        public Tables.Person person;
        public ICommand StartExcersie { get; private set; }
        public ICommand EndExercise { get; private set; }
        public ICommand PauseExcersie { get; private set; }
        protected void OnpropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ProgressPageViewModel(double METvalue)
        {
            _dbModel = new Databasemodel();
            _connection = _dbModel.GetConnection();
            GPS = new geolocation();
            exceriseDBOps = new exceriseDBOps();
            StartExcersie = new Command(async () => await InProgress());
            PauseExcersie = new Command(async ()=> await Pause());
            EndExercise = new Command(async () => await End());
            currentPoint = new Tables.Point();
            stopwatch = new Stopwatch();
            MetValue = METvalue;
            Load();

        }
        async Task Load()
        {
            person = await exceriseDBOps.GetPersonAsync();

        }

        async Task InProgress()
        {
            pauseVisable = true;
            endVisable = true;
            startVisable = false;
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                timer = stopwatch.Elapsed.ToString();
                timer = timer.Substring(0, timer.Length - 8);
                OnpropertyChanged(nameof(Timer));
                 return true;
            });
            
            OnpropertyChanged(nameof(PauseVisable));
            OnpropertyChanged(nameof(EndVisable));
            OnpropertyChanged(nameof(StartVisable));

            inProgress = true;
            var start = await GPS.GPSresquest();//starting point 
            currentPoint.latitude = start.Latitude;
            currentPoint.longatide = start.Longitude;
            currentPoint.timestamp = start.Timestamp.DateTime;
            
            

            while (inProgress)
            {
                var point = await GPS.GPSresquest();
               

                Location newlocation = new Location(point.Latitude, point.Longitude);
                Location currentlocation = new Location(currentPoint.latitude, currentPoint.longatide);
                distance += Location.CalculateDistance(newlocation, currentlocation, DistanceUnits.Kilometers);
                speed = (double)point.Speed;
                currentPoint.speed = (double)point.Speed;
                currentPoint.latitude = point.Latitude;
                currentPoint.longatide = point.Longitude;
                currentPoint.timestamp = point.Timestamp.DateTime;
                if (person.weight != 0)
                {
                   cal += (MetValue * person.weight)/3600;
                }
                else
                {
                    cal += (MetValue * 80)/3600;
                }
                OnpropertyChanged(nameof (CurrentSpeed));
                OnpropertyChanged(nameof(CurrentDistance));
                OnpropertyChanged(nameof(Cal));
                Task.Delay(1000).Wait();
                await exceriseDBOps.AddPoint(currentPoint);
                

            }
        }
        async Task Pause()
        {
            inProgress = false;
            
            stopwatch.Stop();
            startVisable = true;
            pauseVisable = false;
            OnpropertyChanged(nameof(StartVisable));
            OnpropertyChanged(nameof(PauseVisable));
        }

        async Task End()
        {
            inProgress = false;
            stopwatch.Stop();
            await Application.Current.MainPage.DisplayAlert("End", "Exercise has been ended", "OK");

            
            person.calories += cal;
            person.total_dis += distance;
            person.today_dis += distance;

            await exceriseDBOps.UpdatePerson(person);

            await Application.Current.MainPage.Navigation.PopToRootAsync();

        }
        public string  CurrentSpeed
        {
            get
            {
                return "KPH: "+ Math.Round(speed,3);
            }

            set
            {
                
            }
           
        }

        public string CurrentDistance
        {
            get
            {
                return "Kilometres: "+ Math.Round(distance,2);
            }
            
        }


        public string Cal
        {
            get
            {
                return "Calories burned: " + Math.Round(cal, 2);
            }
            set
            {

            }
        }
        public string Timer
        {
            get
            {
                return timer ;
            }
        
        }


        public bool StartVisable
        {
            get
            {
                return startVisable;
            }
        
        }
        public bool PauseVisable
        {
            get
            {
                return pauseVisable;
            }
        
        }

        public bool EndVisable
        {
            get
            {
                return endVisable;
            }
        
        }


    }
}
