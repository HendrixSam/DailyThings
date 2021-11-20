using DailyThings.Models;
using System;
using System.Threading.Tasks;

namespace DailyThings.Services.Implementations {
    /// <summary>
    /// 诗词服务
    /// </summary>
    public class PoetryService : IPoetryService {
        

        /******** 继承方法 ********/
        /// <summary>
        /// 获得诗词
        /// </summary>
        public async Task<Poetry> GetPoetryAsync() {
            throw new NotImplementedException();
        }
    }
}