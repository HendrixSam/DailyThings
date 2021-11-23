using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Services;
using DailyThings.Services.Implementations;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyThings.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e) {
            var poetryService = new PoetryService(
                SimpleIoc.Default.GetInstance<IPreferenceStorage>(),
                SimpleIoc.Default.GetInstance<IAlertService>(),
                SimpleIoc.Default.GetInstance<PoetryStorage>());
            var poetry = await poetryService.GetPoetryAsync();
            Result.Text = poetry.Name;
        }
    }
}