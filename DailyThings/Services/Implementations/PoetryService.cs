using DailyThings.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DailyThings.Utils;
using Newtonsoft.Json;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 诗词服务实现
    /// </summary>
    public class PoetryService : IPoetryService {
        /******** 公有变量 ********/
        /// <summary>
        /// 诗词Token键
        /// </summary>
        public const string PoetryTokenKey = nameof(PoetryService) + ".Token";

        /// <summary>
        /// 诗词服务器
        /// </summary>
        public const string PoetryServer = "诗词服务器";

        /******** 私有变量 ********/
        /// <summary>
        /// 内存里缓存的Token
        /// </summary>
        private string _token;

        /// <summary>
        /// 偏好存储里缓存的Token
        /// </summary>
        private IPreferenceStorage _preferenceStorage;

        /// <summary>
        /// 警告服务
        /// </summary>
        private IAlertService _alertService;

        /******** 继承方法 ********/
        /// <summary>
        /// 获得诗词
        /// </summary>
        public async Task<Poetry> GetPoetryAsync() {
            throw new NotImplementedException();
        }

        /******** 公有方法 ********/

        /// <summary>
        /// 诗词服务构造方法
        /// </summary>
        /// <param name="preferenceStorage">偏好存储</param>
        /// <param name="alertService">警告服务</param>
        public PoetryService(IPreferenceStorage preferenceStorage,
            IAlertService alertService) {
            _preferenceStorage = preferenceStorage;
            _alertService = alertService;
        }

        /******** 私有方法 ********/
        /// <summary>
        /// 获得诗词Token
        /// </summary>
        public async Task<string> GetPoetryTokenAsync() {
            //一级缓存,从内存中读取
            if (!string.IsNullOrEmpty(_token)) {
                return _token;
            }

            //二级缓存,从偏好存储中读取
            _token = _preferenceStorage.Get(PoetryTokenKey, string.Empty);
            if (!string.IsNullOrEmpty(_token)) {
                return _token;
            }

            //三级缓存
            PoetryToken poetryToken;
            using (var httpClient = new HttpClient()) {
                HttpResponseMessage response;
                try {
                    response =
                        await httpClient.GetAsync(
                            "https://v2.jinrishici.com/token");
                    response.EnsureSuccessStatusCode();
                } catch (Exception e) {
                    _alertService.DisPlayAlert(
                        ErrorMessages.HTTP_CLIENT_ERROR_TITLE,
                        ErrorMessages.HttpClientErrorMessage(PoetryServer,
                            e.Message), ErrorMessages.HTTP_CLIENT_ERROR_BUTTON);
                    return _token;
                }

                var json = await response.Content.ReadAsStringAsync();
                poetryToken = JsonConvert.DeserializeObject<PoetryToken>(json);
            }

            if (poetryToken == null)
                return _token;
            _token = poetryToken.Data;
            _preferenceStorage.Set(PoetryTokenKey, _token);
            return _token;
        }
    }

    public class PoetryToken {
        public string Data { get; set; }
    }
}