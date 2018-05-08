using Copter.Logger.Models;

namespace Copter.Logger
{
    /// <summary>
    /// 控制台 日志记录类
    /// </summary>
    public class ConsoleLoggerProvider : ILogger
    {
        public void Log(LogEntity entity)
        {
            System.Console.Write("AppId={0},Title={1},Message={2},StackTrace={3},LevelType={4},CreateTime={5}", entity.AppId, entity.Title, entity.Message, entity.StackTrace, entity.LevelType, entity.CreateTime);
        }
    }
}
