using System;
using System.Collections.Generic;
using System.Text;

namespace DailyThings.Models {
    /// <summary>
    /// 诗词类
    /// </summary>
    [SQLite.Table("Poetry")]
    public class Poetry {
        /// <summary>
        /// 诗词Id
        /// </summary>
        [SQLite.PrimaryKey]
        [SQLite.Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [SQLite.Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [SQLite.Column("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// 朝代
        /// </summary>
        [SQLite.Column("dynasty")]
        public string Dynasty { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        [SQLite.Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// 预览
        /// </summary>
        [SQLite.Column("snippet")]
        public string Snippet { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [SQLite.Column("match_tags")]
        public string MatchTags { get; set; }
    }
}