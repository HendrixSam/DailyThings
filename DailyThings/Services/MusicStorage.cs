using System;
using System.Collections.Generic;
using System.Text;
using DailyThings.Services.Implementations;

namespace DailyThings.Services {
    public class MusicStorage : DataBaseService{
        /// <summary>
        /// 构造方法
        /// </summary>
        public MusicStorage(IPreferenceStorage preferenceStorage) : base(preferenceStorage) { }
    }
}
