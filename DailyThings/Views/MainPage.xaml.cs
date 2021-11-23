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

        private async void PoetryButton_OnClicked(object sender, EventArgs e) {
            var poetryService = new PoetryService(
                SimpleIoc.Default.GetInstance<IPreferenceStorage>(),
                SimpleIoc.Default.GetInstance<IAlertService>(),
                SimpleIoc.Default.GetInstance<PoetryStorage>());
            var poetry = await poetryService.GetPoetryAsync();
            PoetryResult.Text = poetry.Name;
        }

        private async void MusicButton_OnClicked(object sender, EventArgs e) {
            var musicService = new MusicService(SimpleIoc.Default.GetInstance<MusicStorage>(),
                SimpleIoc.Default.GetInstance<IAlertService>());
            var music = await musicService.GetMusicAsync();
            MusicResult.Text = music.Url;
        }
    }
}