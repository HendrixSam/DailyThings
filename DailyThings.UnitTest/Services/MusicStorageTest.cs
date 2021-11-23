using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;
using DailyThings.Services;
using DailyThings.UnitTest.Helpers;
using NUnit.Framework;

namespace DailyThings.UnitTest.Services {
    /// <summary>
    /// 音乐存储测试
    /// </summary>
    public class MusicStorageTest {
        /// <summary>
        /// SetUp单元测试运行前运行,TearDown单元测试运行后运行
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            DataBaseServiceHelper.RemoveDataBaseFile();

        /// <summary>
        /// 测试得到若干音乐
        /// </summary>
        [Test]
        public async Task TestGetPoetryListAsync() {
            var musicStorage =
                await DataBaseServiceHelper.GetInitializedMusicStorageAsync();
            var musicList = await musicStorage.GetMusicListAsync(m => m.MatchTags == "happy",
                0, int.MaxValue);
            Assert.AreEqual(3, musicList.Count);
            await musicStorage.CloseAsync();
        }
    }
}
