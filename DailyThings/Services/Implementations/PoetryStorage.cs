using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;

namespace DailyThings.Services.Implementations {
    public class PoetryStorage : IPoetryStorage {

        /******** 私有变量 ********/

        /// <summary>
        /// 诗词数据库文件名称
        /// </summary>
        public const string PoetryDbName = "poetry.db";

        /// <summary>
        /// 诗词数据库文件路径
        /// </summary>
        public static readonly string PoetryDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), PoetryDbName);

        /// <summary>
        /// 初始化诗词存储
        /// </summary>
        public async Task InitializeAsync() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public bool Initialized() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取一个诗词
        /// </summary>
        /// <param name="id">诗词id</param>
        public async Task<Poetry> GetPoetryAsync(int id) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取满足给定条件的诗词集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        public async Task<IList<Poetry>> GetPoetryListAsync(
            Expression<Func<Poetry, bool>> @where, int skip, int take) {
            throw new NotImplementedException();
        }
    }
}