using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Services;
using DailyThings.Services.Implementations;
using Moq;

namespace DailyThings.UnitTest.Helpers {
    /// <summary>
    /// 诗词存储帮助类
    /// </summary>
    public static class PoetryStorageHelper {
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