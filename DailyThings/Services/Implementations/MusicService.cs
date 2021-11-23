using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;
using DailyThings.Utils;
using Newtonsoft.Json;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 音乐服务接口
    /// </summary>
    public class MusicService : IMusicService{
        /******** 继承方法 ********/

        /// <summary>
        /// 获得一首音乐
        /// </summary>
        public Task<Music> GetMusicAsync() {

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
        }
    }
    

    public class CloudMusic {
        public List<Music> Data { get; set; }
    }

    public class GetUrl {
        public string Url { get; set; }
    }

    public class MusicUrl {
        public List<GetUrl> Data { get; set; }
    }
}
