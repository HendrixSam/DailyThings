namespace DailyThings.UWP {
    public sealed partial class MainPage {
        public MainPage() {
            this.InitializeComponent();

            LoadApplication(new DailyThings.App());
        }
    }
}
