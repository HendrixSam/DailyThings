using DailyThings.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 诗词服务实现
    /// </summary>
    public class PoetryService : IPoetryService {
        /******** 公有变量 ********/
        public const string TokenKey = nameof(PoetryService) + ".Token";

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
        public PoetryService(IPreferenceStorage preferenceStorage, IAlertService alertService) {
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
            _token = _preferenceStorage.Get(TokenKey, String.Empty);
            if (!string.IsNullOrEmpty(_token)) {
                return _token;
            }

            //三级缓存
            using (var httpClient = new HttpClient()) {
                
            }
        }
    }

    public class PoetryToken {
        public string status { get; set; }
        public string data { get; set; }
    }
}