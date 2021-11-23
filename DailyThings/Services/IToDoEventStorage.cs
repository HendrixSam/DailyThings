using System;

namespace DailyThings.Services {
    public class Class1
    {
        /// <summary>
        /// 待办事件存储
        /// </summary>
	    public interface IToDOEventStorage
        {
            /// <summary>
            /// 初始化事件存储
            /// </summary>
            Task InitaliszAsync();

            /// <summary>
            /// 是否初始化
            /// </summary>
            bool Initialized()；

            /// <summary>
            /// 获取一个事件
            /// </summary>
            /// <param name="id">诗词id</param>
            Task<Poetry> GetEventAsync(int id);

            /// <summary>
            /// 事件存储常量
            /// </summary>
            public static class ToDoEventStorageConstants
            {
                /// <summary>
                /// 待办数据库中事件的数量
                /// </summary>
                public const int PoetryNumber = 5;
                /// <summary>
                /// 版本键"PoetryStorageConstants.Version"
                /// </summary>
                public const string VersionKey =
                    nameof(PoetryStorageConstants) + "." + nameof(Version);

                /// <summary>
                /// 版本
                /// </summary>
                public const int Version = 1;

                /// <summary>
                /// 默认版本
                /// </summary>
                public const int DefaultVersion = -1;
            }
        }
    }

}

