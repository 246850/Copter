using Copter.Logger.Models;

namespace Copter.Logger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log 方法
        /// </summary>
        /// <param name="entity">日志 实体</param>
        void Log(LogEntity entity);
    }
}
