using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using fitnessApp.viewModel;
namespace fitnessApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainPageViewModel();
        }
        public MainPageViewModel ViewModel
        {
            get { return BindingContext as MainPageViewModel; }

            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            ViewModel.LoadCommand.Execute(null);
            base.OnAppearing();
        }
        public async void onButtonProfileClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Profile());
        }

        public async void onButtonExerciseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Exercise());
        }

        
    }
}
