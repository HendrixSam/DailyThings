using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;
using DailyThings.Utils;
using Newtonsoft.Json;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 音乐服务实现
    /// </summary>
    public class MusicService : IMusicService {
        /******** 公有变量 ********/
        /// <summary>
        /// 音乐服务器
        /// </summary>
        public const string MusicServer = "音乐服务器";

        /******** 私有变量 ********/

        /// <summary>
        /// 警告服务
        /// </summary>
        private IAlertService _alertService;

        /// <summary>
        /// 偏好存储
        /// </summary>
        private MusicStorage _musicStorage;

        /******** 继承方法 ********/

        /// <summary>
        /// 获得音乐
        /// </summary>
        public async Task<Music> GetMusicAsync() {
            // todo 只传了happy,之后进行修改
            var musicList = await _musicStorage.GetMusicListAsync(m => m.MatchTags == "funny",
                0, int.MaxValue);

            var count = new Random().Next(0, musicList.Count - 1);

            MusicWithUrl musicWithUrl;
            using (var httpClient = new HttpClient()) {
                HttpResponseMessage response;
                try {
                    response = await httpClient.GetAsync(
                        "http://8.140.151.45:3000/song/url?id=" +
                        musicList[count].Id);
                    response.EnsureSuccessStatusCode();
                } catch (Exception e) {
                    _alertService.DisplayAlert(
                        ErrorMessages.HttpClientErrorTitle,
                        ErrorMessages.HttpClientErrorMessage(MusicServer,
                            e.Message), ErrorMessages.HttpClientErrorButton);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                musicWithUrl =
                    JsonConvert.DeserializeObject<MusicWithUrl>(json);
            }

            if (musicWithUrl == null) {
                return null;
            }

            return new Music {
                Artist = musicList[count].Artist,
                Id = musicList[count].Id,
                MatchTags = musicList[count].MatchTags,
                Name = musicList[count].Name,
                Url = musicWithUrl.Data[0].Url + ".mp3"
            };
        }

        /******** 公有方法 ********/

        /// <summary>
        /// 音乐服务构造方法
        /// </summary>
        /// <param name="musicStorage">偏好存储</param>
        /// <param name="alertService">警告服务</param>
        public MusicService(MusicStorage musicStorage,
            IAlertService alertService) {
            _musicStorage = musicStorage;
            _alertService = alertService;
        }
    }

    public class GetUrl {
        public string Url { get; set; }
    }

    public class MusicWithUrl {
        public List<GetUrl> Data { get; set; }
    }
}