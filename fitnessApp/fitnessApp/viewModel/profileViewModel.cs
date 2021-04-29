using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SQLite;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Essentials;
using fitnessApp.models;

namespace fitnessApp.viewModel
{
  public class profileViewModel : INotifyPropertyChanged
  {
        private readonly exceriseDBOps _excericeDBOps;
        private SQLiteAsyncConnection _connection;
        private Databasemodel _dbModel;

        public Tables.Person person;        
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public profileViewModel()
        {
            _dbModel = new Databasemodel();
            _connection = _dbModel.GetConnection();
            _connection.CreateTableAsync<Tables.Person>();


            person = new Tables.Person();
            _excericeDBOps = new exceriseDBOps();
            SaveCommand = new Command(async () => await Save());
            LoadCommand = new Command(async () => await Load());


        }
      
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnpropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(person.first_name) || String.IsNullOrWhiteSpace(person.second_name) || person.age <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter all infomation", "OK");
                return;
            }
            await _excericeDBOps.UpdatePerson(person);

            await Application.Current.MainPage.Navigation.PopToRootAsync();
            return;
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
            else
            {
                OnpropertyChanged(nameof(First_name));
                OnpropertyChanged(nameof(Age));
                OnpropertyChanged(nameof(Second_name));
            }
        }

        public string First_name
        {
            get
            { 
                return person.first_name;
            } 
            set
            {
                person.first_name = value;
                OnpropertyChanged();
            } 
        }
        public string Second_name
        {
            get
            {
                return person.second_name;
            }
            set
            {
                person.second_name = value;
                OnpropertyChanged();
            }

        }
        public int Age
        {
            get
            {
                return person.age;
            }
            set
            {
                person.age = value;
                OnpropertyChanged();
            }
        }
      
        public double Weight
        {
            get
            {
                return person.weight;
            }
            set
            {
                person.weight = value;
                OnpropertyChanged();
            }
        }
        public double Height
        {
            get
            {
                return person.height;
            }
            set
            {
                person.height = value;
                OnpropertyChanged();
            }
        }

  }
}
