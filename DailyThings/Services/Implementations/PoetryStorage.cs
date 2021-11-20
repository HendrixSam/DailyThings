using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DailyThings.Models;
using SQLite;
using Xamarin.Essentials;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 诗词存储实现
    /// </summary>
    public class PoetryStorage : IPoetryStorage {
        /******** 公有变量 ********/

        /// <summary>
        /// 诗词数据库文件路径
        /// </summary>
        public static readonly string PoetryDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), PoetryDbName);

        /******** 私有变量 ********/

        /// <summary>
        /// 诗词数据库文件名称
        /// </summary>
        private const string PoetryDbName = "poetry.db";

        /// <summary>
        /// 数据库连接
        /// </summary>
        private SQLiteAsyncConnection _connection;

        public SQLiteAsyncConnection Connection =>
            _connection ??
            (_connection = new SQLiteAsyncConnection(PoetryDbName));

        /******** 继承方法 ********/

        /// <summary>
        /// 初始化诗词存储
        /// </summary>
        public async Task InitializeAsync() {
            using (var dbFileStream =
                new FileStream(PoetryDbPath, FileMode.Create)) {
                using (var dbAssertStream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(PoetryDbName)) {
                    if (dbAssertStream != null) {
                        await dbAssertStream.CopyToAsync(dbFileStream);
                    }
                }
            }

            Preferences.Set(PoetryStorageConstants.VersionKey,
                PoetryStorageConstants.Version);
        }

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public bool Initialized() =>
            Preferences.Get(PoetryStorageConstants.VersionKey,
                PoetryStorageConstants.DefaultVersion) ==
            PoetryStorageConstants.Version;


        /// <summary>
        /// 获取一个诗词
        /// </summary>
        /// <param name="id">诗词id</param>
        public async Task<Poetry> GetPoetryAsync(int id) =>
            await Connection.Table<Poetry>()
                .FirstOrDefaultAsync(p => p.Id == id);

        /// <summary>
        /// 获取满足给定条件的诗词集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        public async Task<IList<Poetry>> GetPoetryListAsync(
            Expression<Func<Poetry, bool>> @where, int skip, int take) =>
            await Connection.Table<Poetry>().Where(@where).Skip(skip).Take(take)
                .ToListAsync();
    }
}