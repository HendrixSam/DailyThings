using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Services.Implementations;
using NUnit.Framework;
using Xamarin.Essentials;

namespace DailyThings.UnitTest.Services {
    /// <summary>
    /// 诗词存储测试
    /// </summary>
    public class PoetryStorageTest : SystemException {
        /// <summary>
        /// 测试初始化诗词存储
        /// </summary>
        [Test]
        public async Task TestInitializeAsync() {
            //Preferences.Set("key", "value");
            Assert.IsFalse(File.Exists(PoetryStorage.PoetryDbPath));//没有初始化前PoetryDbPath一定不存在
            var poetryStorage = new PoetryStorage();
            await poetryStorage.InitializeAsync();//进行初始化
            Assert.IsTrue(File.Exists(PoetryStorage.PoetryDbPath));
            
        }
    }

}