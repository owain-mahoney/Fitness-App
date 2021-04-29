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
    public partial class ProgressPage : ContentPage
    {
        public ProgressPage(ProgressPageViewModel progressPageViewModel)
        {
            InitializeComponent();
            ViewModel = progressPageViewModel;
        }

        public ProgressPageViewModel ViewModel
        {
            get
            {
                return BindingContext as ProgressPageViewModel;
            }
            set
            {
                BindingContext = value;
            }

        }

       
    }
}