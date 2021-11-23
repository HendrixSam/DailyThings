using System;
using System.Collections.Generic;
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
    /// 诗词服务测试
    /// </summary>
    public class PoetryServiceTest {
        /// <summary>
        /// SetUp单元测试运行前运行,TearDown单元测试运行后运行
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            DataBaseServiceHelper.RemoveDataBaseFile();

        //[Test]
        //public async Task TestGetRandomPoetryAsync() {
        //    var poetryStorage = await DataBaseServiceHelper.GetInitializedPoetryStorageAsync();
        //    var poetryService = new PoetryService(null, null, poetryStorage);
        //    var randomPoetry = await poetryService.GetRandomPoetryAsync();
        //    Assert.IsNotNull(randomPoetry);
        //    Assert.IsFalse(string.IsNullOrWhiteSpace(randomPoetry.Name));
        //    await poetryStorage.CloseAsync();

        //}
    }
}
