using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Essentials;
using System.Diagnostics;
using SQLite;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using fitnessApp.models;

namespace fitnessApp.viewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly exceriseDBOps _excericeDBOps;
        private SQLiteAsyncConnection _connection;
        private Databasemodel _dbModel;
        public ICommand LoadCommand { get; private set; }

        public DateTime day;
        public Tables.Person person;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            _dbModel = new Databasemodel();
            _connection = _dbModel.GetConnection();
            _connection.CreateTableAsync<Tables.Person>();

            person = new Tables.Person();
            _excericeDBOps = new exceriseDBOps();
            LoadCommand = new Command(async () => await Load());

        }

        void OnProperyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        async Task Load()
        {

            person = await _excericeDBOps.GetPersonAsync();

            if (person == null)
            {
                Tables.Person newPerson = new Tables.Person();

                await _excericeDBOps.InsertPerson(newPerson);
                person = newPerson;
            }
                            
           
            if (person.currentDay.Date.Day < DateTime.Now.Date.Day)
            {
                person.currentDay = DateTime.Now;
                person.today_dis = 0;
                person.calories = 0;
                await _excericeDBOps.UpdatePerson(person);
            }

            OnProperyChanged(nameof(Total_dis));
            OnProperyChanged(nameof(Today_dis));
            OnProperyChanged(nameof(Cal));
        }

        
        public string  Total_dis
        {
            get { return "Kilometres: " + Math.Round(person.total_dis, 2); }
            set
            {
              
            }
        }

       public string Today_dis
        {
            get
            {
                return "Kilometres: " + Math.Round(person.today_dis, 2);
            }
            set
            {

            }
        
        }

        public string Cal
        {
            get {return "Calories : " + Math.Round(person.calories, 2); }
            set
            {

            }
        
        }


        
    }
}
