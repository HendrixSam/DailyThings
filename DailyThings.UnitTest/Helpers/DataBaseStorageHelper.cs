using DailyThings.Services;
using DailyThings.Services.Implementations;
using Moq;
using System.IO;
using System.Threading.Tasks;

namespace DailyThings.UnitTest.Helpers {
    /// <summary>
    /// 数据库存储帮助类
    /// </summary>
    public static class DataBaseStorageHelper {

        /// <summary>
        /// 获得已初始化的数据库存储
        /// </summary>
        public static async Task<DataBaseStorage>
            GetInitializedPoetryStorageAsync() {
            var dataBaseStorage =
                new DataBaseStorage(new Mock<IPreferenceStorage>().Object);
            await dataBaseStorage.InitializeAsync();
            return dataBaseStorage;
        }

        /// <summary>
        /// 删除单元测试产生的数据库文件
        /// </summary>
        public static void RemoveDataBaseFile() =>
            File.Delete(DataBaseStorage.DailyThingsDbPath); //自动删除单元测试的文件
    }
}