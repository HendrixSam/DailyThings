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
            DataBaseServiceHelper.RemoveDataBaseFile();
        /// <summary>
        /// 测试得到一首诗词
        /// </summary>
        [Test]
        public async Task TestGetPoetryAsync() {
            var poetryStorage =
                await DataBaseServiceHelper.GetInitializedPoetryStorageAsync();
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
                await DataBaseServiceHelper.GetInitializedPoetryStorageAsync();
            var poetryList = await poetryStorage.GetPoetryListAsync(
                Expression.Lambda<Func<Poetry, bool>>(Expression.Constant(true),
                    Expression.Parameter(typeof(Poetry), "p")), 0,
                int.MaxValue);//这个条件表示任何都满足
            Assert.AreEqual(DailyThingsServiceConstants.PoetryNumber, poetryList.Count);
            await poetryStorage.CloseAsync();
        }
    }
}