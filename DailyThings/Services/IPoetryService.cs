using DailyThings.Models;
using System.Threading.Tasks;

namespace DailyThings.Services {
    /// <summary>
    /// 诗词服务接口
    /// </summary>
    public interface IPoetryService {
        /// <summary>
        /// 获得诗词
        /// </summary>
        Task<Poetry> GetPoetryAsync();

    }
}
