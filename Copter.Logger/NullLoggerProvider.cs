using Copter.Logger.Models;

namespace Copter.Logger
{
    /// <summary>
    /// 空日志记录实现类
    /// </summary>
    public class NullLoggerProvider:ILogger
    {
        public void Log(LogEntity entity)
        {
        }
    }
}
