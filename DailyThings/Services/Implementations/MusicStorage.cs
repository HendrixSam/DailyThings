using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;
using DailyThings.Services.Implementations;

namespace DailyThings.Services.Implementations {
    public class MusicStorage : DataBaseService {
        /******** 公有方法 ********/

        /// <summary>
        /// 获取满足给定条件的音乐集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        public async Task<IList<Music>> GetMusicListAsync(
            Expression<Func<Music, bool>> @where, int skip, int take) =>
            await Connection.Table<Music>().Where(@where).Skip(skip).Take(take)
                .ToListAsync();


        /// <summary>
        /// 构造方法
        /// </summary>
        public MusicStorage(IPreferenceStorage preferenceStorage) : base(
            preferenceStorage) { }
    }
}