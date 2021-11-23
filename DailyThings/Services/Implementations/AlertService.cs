using System;
using System.Collections.Generic;
using System.Text;
using DailyThings.Views;
using Xamarin.Forms;

namespace DailyThings.Services.Implementations {
    public class AlertService : IAlertService {
        /******** 私有变量 ********/

        /// <summary>
        /// 用于显示警告的MainPage。
        /// </summary>
        private MainPage MainPage =>
            _mainPage ?? (_mainPage = Application.Current.MainPage as MainPage);
        
        /// <summary>
        /// 用于显示警告的MainPage。
        /// </summary>
        private MainPage _mainPage;

        /******** 继承方法 ********/

        /// <summary>
        /// 显示警告
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="button">按钮文字</param>
        public void DisplayAlert(string title, string content, string button) =>
            Device.BeginInvokeOnMainThread(() =>
                MainPage.DisplayAlert(title, content, button));
    }
}