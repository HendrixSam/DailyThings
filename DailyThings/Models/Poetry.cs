using System;
using System.Collections.Generic;
using System.Text;

namespace DailyThings.Models {
    /// <summary>
    /// 诗词类
    /// </summary>
    public class Poetry {
        /// <summary>
        /// 预览
        /// </summary>
        public string Snippet { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 朝代
        /// </summary>
        public string Dynasty { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string MatchTags { get; set; }
    }
}
