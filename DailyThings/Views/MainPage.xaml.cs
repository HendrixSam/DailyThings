using System;
using System.Runtime.InteropServices;
using DailyThings.Services;
using DailyThings.Services.Implementations;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

namespace DailyThings {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e) {
            PoetryService poetryService = new PoetryService(
                SimpleIoc.Default.GetInstance<IPreferenceStorage>(),
                SimpleIoc.Default.GetInstance<IAlertService>(),
                SimpleIoc.Default.GetInstance<IPoetryStorage>());
            var poetry = await poetryService.GetPoetryAsync();
            Result.Text = poetry.Name;
        }
    }
}