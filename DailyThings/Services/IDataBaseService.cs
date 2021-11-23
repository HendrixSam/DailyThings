using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DailyThings.Services {
    /// <summary>
    /// 数据库存储接口
    /// </summary>
    public interface IDataBaseService {
        /// <summary>
        /// 初始化DailyThings存储
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        bool Initialized();
    }

    /// <summary>
    /// DailyThings存储常量
    /// </summary>
    public static class DailyThingsServiceConstants {
        /// <summary>
        /// 诗词数据库中诗词的数量
        /// </summary>
        public const int PoetryNumber = 30;

        /// <summary>
        /// 音乐数据库中音乐的数量
        /// </summary>
        public const int MusicNumber = 8;

        /// <summary>
        /// 版本键"DailyThingsStorageConstants.Version"
        /// </summary>
        public const string VersionKey =
            nameof(DailyThingsServiceConstants) + "." + nameof(Version);

        /// <summary>
        /// 版本
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 默认版本
        /// </summary>
        public const int DefaultVersion = -1;
    }
}
