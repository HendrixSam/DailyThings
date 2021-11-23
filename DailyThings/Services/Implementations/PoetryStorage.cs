using DailyThings.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 诗词存储实现
    /// </summary>
    public class PoetryStorage : DataBaseStorage {

        /******** 公有方法 ********/

        /// <summary>
        /// 获取一首诗词
        /// </summary>
        /// <param name="id">诗词id</param>
        public async Task<Poetry> GetPoetryAsync(int id) =>
            await Connection.Table<Poetry>()
                .FirstOrDefaultAsync(p => p.Id == id);

        /// <summary>
        /// 获取满足给定条件的诗词集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        public async Task<IList<Poetry>> GetPoetryListAsync(
            Expression<Func<Poetry, bool>> @where, int skip, int take) =>
            await Connection.Table<Poetry>().Where(@where).Skip(skip).Take(take)
                .ToListAsync();

        /******** 公有方法 ********/

        /// <summary>
        /// 构造方法
        /// </summary>
        public PoetryStorage(IPreferenceStorage preferenceStorage) : base(preferenceStorage) {
            Preference = preferenceStorage;
        }

    }
}