using System;
using DailyThings.Services;
using DailyThings.Services.Implementations;
using DailyThings.UnitTest.Helpers;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DailyThings.Models;

namespace DailyThings.UnitTest.Services {
    /// <summary>
    /// 诗词存储测试
    /// </summary>
    public class PoetryStorageTest {
        /// <summary>
        /// SetUp单元测试运行前运行,TearDown单元测试运行后运行
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            PoetryStorageHelper.RemoveDataBaseFile();

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
            preferenceStorageMock.Verify(
                p => p.Set(PoetryStorageConstants.VersionKey,
                    PoetryStorageConstants.Version),
                Times.Once); // 测试版本号的set是否调用过一次
        }

        /// <summary>
        /// 测试诗词存储是否已经初始化
        /// </summary>
        [Test]
        public void TestIsInitialized() {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(PoetryStorageConstants.VersionKey,
                    PoetryStorageConstants.DefaultVersion))
                .Returns(PoetryStorageConstants
                    .Version); // 如果调用了Get方法就返回PoetryStorageConstants.Version
            var mockPreferenceService = preferenceStorageMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceService);
            Assert.IsTrue(poetryStorage.Initialized());
        }

        /// <summary>
        /// 测试诗词存储没有被初始化
        /// </summary>
        [Test]
        public void TestIsNotInitialized() {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(PoetryStorageConstants.VersionKey,
                    PoetryStorageConstants.DefaultVersion))
                .Returns(PoetryStorageConstants.Version - 1);
            var mockPreferenceService = preferenceStorageMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceService);
            Assert.IsFalse(poetryStorage.Initialized());
        }

        /// <summary>
        /// 测试得到一首诗词
        /// </summary>
        [Test]
        public async Task TestGetPoetryAsync() {
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();
            var poetry = await poetryStorage.GetPoetryAsync(10001);
            Assert.AreEqual("临江仙 · 夜归临皋", poetry.Name);
            await poetryStorage.CloseAsync();
        }

        /// <summary>
        /// 获取满足给定条件的诗词集合
        /// </summary>
        [Test]
        public async Task TestGetPoetryListAsync() {
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();
            var poetryList = await poetryStorage.GetPoetryListAsync(
                Expression.Lambda<Func<Poetry, bool>>(Expression.Constant(true),
                    Expression.Parameter(typeof(Poetry), "p")), 0,
                int.MaxValue);//这个条件表示任何都满足
            Assert.AreEqual(PoetryStorageConstants.PoetryNumber, poetryList.Count);
            await poetryStorage.CloseAsync();
        }
    }
}