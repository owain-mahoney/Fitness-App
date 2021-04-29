using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fitnessApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        public Exercise()
        {
            InitializeComponent();
        }
        public async void onButtonHomeClicked(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();

        }
        public async void onButtonProfileClicked(object sender, EventArgs args)
        {
           
            await Navigation.PushAsync(new Profile());
        }

        private async void RunningImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProgressPage(new viewModel.ProgressPageViewModel(9.8)));
        }
        private async void WalkingImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProgressPage(new viewModel.ProgressPageViewModel(4.3)));
        }
        private async void CyclingImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProgressPage(new viewModel.ProgressPageViewModel(8.0)));
        }
    }
}