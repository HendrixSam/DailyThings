using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DailyThings.Models;
using DailyThings.Services.Implementations;

namespace DailyThings.Services.Implementations {
    public class MusicStorage : DataBaseService{
        /******** 公有方法 ********/

        /// <summary>
        /// 获取一首音乐
        /// </summary>
        /// <param name="matchTags">音乐标签</param>
        public async Task<IList<Music>> GetMusicAsync(string matchTags) =>
            await Connection.Table<Music>()
                .Where(m => m.MatchTags == matchTags).ToListAsync();


        /// <summary>
        /// 构造方法
        /// </summary>
        public MusicStorage(IPreferenceStorage preferenceStorage) : base(preferenceStorage) { }
    }
}
