using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Services;
using DailyThings.Services.Implementations;
using Moq;
using NUnit.Framework;
using Xamarin.Essentials;

namespace DailyThings.UnitTest.Services {
    /// <summary>
    /// 诗词存储测试
    /// </summary>
    public class PoetryStorageTest {
        [SetUp, TearDown] //SetUp单元测试运行前运行，TearDown单元测试运行后运行
        public static void RemoveDatabaseFile() {
            File.Delete(PoetryStorage.PoetryDbPath); //自动删除单元测试的文件
        }

        /// <summary>
        /// 测试初始化诗词存储
        /// </summary>
        [Test]
        public async Task TestInitializeAsync() {
            Assert.IsFalse(
                File.Exists(PoetryStorage
                    .PoetryDbPath)); //没有初始化前PoetryDbPath一定不存在
            var preferenceStorageMock = new Mock<IPreferenceStorage>(); //mock工具
            var mockPreferenceService = preferenceStorageMock.Object; //得到的mock
            var poetryStorage = new PoetryStorage(mockPreferenceService);
            await poetryStorage.InitializeAsync(); //进行初始化
            Assert.IsTrue(File.Exists(PoetryStorage.PoetryDbPath));
            preferenceStorageMock.Verify(p =>
                p.Set(PoetryStorageConstants.VersionKey,
                    PoetryStorageConstants.Version), Times.Once);// 测试版本号的set是否调用过一次
        }
    }
}