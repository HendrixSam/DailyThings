using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DailyThings.Models;

namespace DailyThings.Services {
    /// <summary>
    /// 诗词存储接口
    /// </summary>
    public interface IPoetryStorage {
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        bool Initialized();

        /// <summary>
        /// 初始化诗词存储
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 获取一个诗词
        /// </summary>
        /// <param name="id">诗词id</param>
        Task<Poetry> GetPoetryAsync(int id);

        /// <summary>
        /// 获取满足给定条件的诗词集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        Task<IList<Poetry>> GetPoetryListAsync(
            Expression<Func<Poetry, bool>> where, int skip, int take);
    }

    /// <summary>
    /// 诗词存储常量
    /// </summary>
    public static class PoetryStorageConstants {
        /// <summary>
        /// 版本键
        /// </summary>
        public const string VersionKey =
            nameof(PoetryStorageConstants) + "." + nameof(Version);

        /// <summary>
        /// 版本
        /// </summary>
        public const int Version = 1;
    }
}