using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 数据库存储实现
    /// </summary>
    public class DataBaseStorage : IDataBaseStorage{
        /******** 公有变量 ********/

        /// <summary>
        /// 诗词数据库文件路径
        /// </summary>
        public static readonly string DailyThingsDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), DailyThingsDbName);

        /******** 私有变量 ********/

        /// <summary>
        /// 诗词数据库文件名称
        /// </summary>
        protected const string DailyThingsDbName = "DailyThings.db";

        /// <summary>
        /// 数据库连接
        /// </summary>
        private SQLiteAsyncConnection _connection;

        protected SQLiteAsyncConnection Connection =>
            _connection ??
            (_connection = new SQLiteAsyncConnection(DailyThingsDbPath));

        /// <summary>
        /// 偏好存储
        /// </summary>
        protected IPreferenceStorage Preference;

        /******** 继承方法 ********/

        /// <summary>
        /// 初始化DailyThings存储
        /// </summary>
        public async Task InitializeAsync() {
            using (var dbFileStream =
                new FileStream(DailyThingsDbPath, FileMode.Create))
            using (var dbAssertStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(DailyThingsDbName)) {
                if (dbAssertStream != null) {
                    await dbAssertStream.CopyToAsync(dbFileStream);
                }
            }

            Preference.Set(DailyThingsStorageConstants.VersionKey,
                DailyThingsStorageConstants.Version);
        }

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public bool Initialized() =>
            Preference.Get(DailyThingsStorageConstants.VersionKey,
                DailyThingsStorageConstants.DefaultVersion) ==
            DailyThingsStorageConstants.Version;

        /******** 公有方法 ********/

        /// <summary>
        /// 构造方法
        /// </summary>
        public DataBaseStorage(IPreferenceStorage preferenceStorage) {
            Preference = preferenceStorage;
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public async Task CloseAsync() => await Connection.CloseAsync();
    }
}
