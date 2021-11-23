using System;
using System.Collections.Generic;
using System.Text;

namespace DailyThings.Models {
    /// <summary>
    /// 音乐类
    /// </summary>
    [SQLite.Table("Music")]
    public class Music {
        /// <summary>
        /// 音乐Id
        /// </summary>
        [SQLite.PrimaryKey]
        [SQLite.Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 音乐名称
        /// </summary>
        [SQLite.Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        [SQLite.Column("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [SQLite.Column("match_tags")]
        public string MatchTags { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [SQLite.Ignore]
        public string Url { get; set; }
    }
}
