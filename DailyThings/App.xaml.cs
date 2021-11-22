using DailyThings.Views;
using Xamarin.Forms;

namespace DailyThings {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
