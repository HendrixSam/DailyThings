using DailyThings.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        /// <summary>
        /// 诗词存储。
        /// </summary>
        private IPoetryStorage _poetryStorage;

        /******** 继承方法 ********/
        /// <summary>
        /// 获得诗词
        /// </summary>
        public async Task<Poetry> GetPoetryAsync() {
            var token = await GetPoetryTokenAsync();
            if (string.IsNullOrEmpty(token)) {
                return await GetRandomPoetryAsync();
            }

            PoetrySentence poetrySentence;
            using (var httpClient = new HttpClient()) {
                var headers = httpClient.DefaultRequestHeaders;
                headers.Add("X-User-Token", token);

                HttpResponseMessage response;
                try {
                    response =
                        await httpClient.GetAsync(
                            "https://v2.jinrishici.com/sentence");
                    response.EnsureSuccessStatusCode();
                } catch (Exception e) {
                    _alertService.DisplayAlert(
                        ErrorMessages.HttpClientErrorTitle,
                        ErrorMessages.HttpClientErrorMessage(PoetryServer,
                            e.Message), ErrorMessages.HttpClientErrorButton);
                    return await GetRandomPoetryAsync();
                }

                var json = await response.Content.ReadAsStringAsync();
                poetrySentence =
                    JsonConvert.DeserializeObject<PoetrySentence>(json);
            }

            if (poetrySentence != null) {
                return new Poetry {
                    Snippet = poetrySentence.Data.Content,
                    Name = poetrySentence.Data.Origin.Title,
                    Dynasty = poetrySentence.Data.Origin.Dynasty,
                    AuthorName = poetrySentence.Data.Origin.Author,
                    Content =
                        string.Join("\n", poetrySentence.Data.Origin.Content),
                    MatchTags = string.Join("\n", poetrySentence.Data.MatchTags)
                };
            }

            return await GetRandomPoetryAsync();
        }

        /******** 公有方法 ********/

        /// <summary>
        /// 诗词服务构造方法
        /// </summary>
        /// <param name="preferenceStorage">偏好存储</param>
        /// <param name="alertService">警告服务</param>
        /// <param name="poetryStorage">诗词存储</param>
        public PoetryService(IPreferenceStorage preferenceStorage,
            IAlertService alertService, IPoetryStorage poetryStorage) {
            _preferenceStorage = preferenceStorage;
            _alertService = alertService;
            _poetryStorage = poetryStorage;
        }

        /******** 私有方法 ********/
        /// <summary>
        /// 获得诗词Token
        /// </summary>
        private async Task<string> GetPoetryTokenAsync() {
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
                    _alertService.DisplayAlert(
                        ErrorMessages.HttpClientErrorTitle,
                        ErrorMessages.HttpClientErrorMessage(PoetryServer,
                            e.Message), ErrorMessages.HttpClientErrorButton);
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

        /// <summary>
        /// 获得随机诗词
        /// </summary>
        private async Task<Poetry> GetRandomPoetryAsync() {
            var poetryList = await _poetryStorage.GetPoetryListAsync(
                Expression.Lambda<Func<Poetry, bool>>(Expression.Constant(true),
                    Expression.Parameter(typeof(Poetry), "p")),
                new Random().Next(PoetryStorageConstants.PoetryNumber), 1);
            var poetry = poetryList[0];
            return new Poetry {
                Snippet = poetry.Snippet,
                Name = poetry.Name,
                Dynasty = poetry.Dynasty,
                AuthorName = poetry.AuthorName,
                Content = poetry.Content,
                MatchTags = poetry.MatchTags
            };
        }
    }

    public class PoetryToken {
        public string Data { get; set; }
    }

    public class PoetryOrigin {
        public string Title { get; set; }
        public string Dynasty { get; set; }
        public string Author { get; set; }
        public List<string> Content { get; set; }
    }

    public class PoetryData {
        public string Content { get; set; }
        public PoetryOrigin Origin { get; set; }
        public List<string> MatchTags { get; set; }
    }

    public class PoetrySentence {
        public PoetryData Data { get; set; }
    }
}