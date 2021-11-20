using DailyThings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DailyThings.Services {
    public interface IPoetryService {
        /// <summary>
        /// 获得诗词
        /// </summary>
        Task<Poetry> GetPoetryAsync();

    }
}
