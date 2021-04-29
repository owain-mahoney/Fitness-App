using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fitnessApp.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fitnessApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            ViewModel = new profileViewModel();
        }

        public profileViewModel ViewModel
        {
            get { return BindingContext as profileViewModel; }

            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            ViewModel.LoadCommand.Execute(null);
            base.OnAppearing();
        }
        public async void onButtonHomeClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();

        }
        public async void onButtonExerciseClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new Exercise());
        }

    }
}