using System;
using System.Runtime.CompilerServices;

namespace Copter.Ioc
{
    public class EngineContext
    {
        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="forceRecreate">是否强制初始化</param>
        /// <param name="engine">Ioc引擎实例</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(IEngine engine, bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = engine;
                Singleton<IEngine>.Instance.Initialize(null);
            }
            return Singleton<IEngine>.Instance;
        }

        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        /// <summary>
        /// 当前引擎实例对象
        /// </summary>
        public static IEngine Current
        {
            get
            {
                IEngine engine = Singleton<IEngine>.Instance;
                if (engine == null) throw new Exception("未初始化Ioc引擎");
                return engine;
            }
        }
    }
}
