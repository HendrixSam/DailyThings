using DailyThings.Services;
using DailyThings.Services.Implementations;
using Moq;
using System.IO;
using System.Threading.Tasks;

namespace DailyThings.UnitTest.Helpers {
    /// <summary>
    /// 诗词存储帮助类
    /// </summary>
    public static class PoetryStorageHelper {
        /******** 公有变量 ********/

        /// <summary>
        /// 诗词数据库中诗词的数量
        /// </summary>
        public const int NumberPoetry = 30; 

        /// <summary>
        /// 获得已初始化的诗词存储
        /// </summary>
        public static async Task<PoetryStorage>
            GetInitializedPoetryStorageAsync() {
            var poetryStorage =
                new PoetryStorage(new Mock<IPreferenceStorage>().Object);
            await poetryStorage.InitializeAsync();
            return poetryStorage;
        }

        /// <summary>
        /// 删除单元测试产生的数据库文件
        /// </summary>
        public static void RemoveDataBaseFile() =>
            File.Delete(PoetryStorage.PoetryDbPath); //自动删除单元测试的文件
    }
}