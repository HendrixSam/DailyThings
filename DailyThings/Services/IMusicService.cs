using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;

namespace DailyThings.Services {
    /// <summary>
    /// 音乐服务
    /// </summary>
    public interface IMusicService {
        /// <summary>
        /// 获得音乐
        /// </summary>
        Task<Music> GetMusicAsync();
    }
}
