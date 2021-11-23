using DailyThings.Services;
using DailyThings.Services.Implementations;
using GalaSoft.MvvmLight.Ioc;

namespace DailyThings.ViewModels {
    /// <summary>
    /// ViewModel Locator
    /// </summary>
    public class ViewModelLocator {
        /// <summary>
        /// 主页
        /// </summary>
        public MainPageViewModel MainPageViewModel =>
            SimpleIoc.Default.GetInstance<MainPageViewModel>();

        /// <summary>
        /// ViewModel Locator
        /// </summary>
        public ViewModelLocator() {
            //注册
            SimpleIoc.Default.Register<PoetryStorage>();
            SimpleIoc.Default.Register<MusicStorage>();
            SimpleIoc.Default.Register<IPoetryService, PoetryService>();
            SimpleIoc.Default.Register<IMusicService, MusicService>();
            SimpleIoc.Default.Register<IAlertService, AlertService>();
            SimpleIoc.Default.Register<IPreferenceStorage, PreferenceStorage>();
            SimpleIoc.Default.Register<MainPageViewModel>();
        }
    }
}