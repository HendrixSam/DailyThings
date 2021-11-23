using System;
using System.Collections.Generic;
using System.Text;

namespace DailyThings.Models {
    /// <summary>
    /// 音乐类
    /// </summary>
    public class Music {
        /// <summary>
        /// 音乐Id
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// 音乐名称
        /// </summary>
        public string SongName { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}
