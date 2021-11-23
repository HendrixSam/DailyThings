using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Services;
using DailyThings.Services.Implementations;
using DailyThings.UnitTest.Helpers;
using Moq;
using NUnit.Framework;

namespace DailyThings.UnitTest.Services {
    /// <summary>
    /// 数据库存储测试
    /// </summary>
    public class DataBaseServiceTest {
        /// <summary>
        /// SetUp单元测试运行前运行,TearDown单元测试运行后运行
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            DataBaseServiceHelper.RemoveDataBaseFile();

        /// <summary>
        /// 测试初始化存储
        /// </summary>
        [Test]
        public async Task TestInitializeAsync() {
            Assert.IsFalse(
                File.Exists(DataBaseService
                    .DailyThingsDbPath)); //没有初始化前PoetryDbPath一定不存在
            var preferenceStorageMock = new Mock<IPreferenceStorage>(); //mock工具
            var mockPreferenceService = preferenceStorageMock.Object; //得到的mock
            var dataBaseStorage = new DataBaseService(mockPreferenceService);
            await dataBaseStorage.InitializeAsync(); //进行初始化
            Assert.IsTrue(File.Exists(DataBaseService.DailyThingsDbPath));
            preferenceStorageMock.Verify(
                p => p.Set(DailyThingsServiceConstants.VersionKey,
                    DailyThingsServiceConstants.Version),
                Times.Once); // 测试版本号的set是否调用过一次
        }

        /// <summary>
        /// 测试数据库存储是否已经初始化
        /// </summary>
        [Test]
        public void TestIsInitialized() {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(DailyThingsServiceConstants.VersionKey,
                    DailyThingsServiceConstants.DefaultVersion))
                .Returns(DailyThingsServiceConstants
                    .Version); // 如果调用了Get方法就返回PoetryStorageConstants.Version
            var mockPreferenceService = preferenceStorageMock.Object;
            var dataBaseStorage = new DataBaseService(mockPreferenceService);
            Assert.IsTrue(dataBaseStorage.Initialized());
        }

        /// <summary>
        /// 测试数据库存储没有被初始化
        /// </summary>
        [Test]
        public void TestIsNotInitialized() {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(DailyThingsServiceConstants.VersionKey,
                    DailyThingsServiceConstants.DefaultVersion))
                .Returns(DailyThingsServiceConstants.Version - 1);
            var mockPreferenceService = preferenceStorageMock.Object;
            var dataBaseStorage = new DataBaseService(mockPreferenceService);
            Assert.IsFalse(dataBaseStorage.Initialized());
        }
    }
}
