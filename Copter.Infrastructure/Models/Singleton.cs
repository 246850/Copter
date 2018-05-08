using System;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 延迟加载单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static volatile Lazy<T> _lazy = new Lazy<T>(() => new T());
        public static T Instance
        {
            get { return _lazy.Value; }
        }
    }
}
