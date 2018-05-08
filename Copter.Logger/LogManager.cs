using System.Threading.Tasks;
using Copter.Logger.Models;

namespace Copter.Logger
{
    /// <summary>
    /// 日志记录管理类
    /// </summary>
    public class LogManager
    {
        protected virtual ILogger Log { get; }
        public LogManager():this(new NullLoggerProvider())
        {
            
        }
        public LogManager(ILogger log)
        {
            Log = log;
        }

        public virtual void Debug(LogEntity entity)
        {
            if (entity == null) return;
            entity.LevelType = LogLevelType.Debug;

            Write(entity);
        }
        public virtual void Info(LogEntity entity)
        {
            if (entity == null) return;
            entity.LevelType = LogLevelType.Info;

            Write(entity);
        }
        public virtual void Warn(LogEntity entity)
        {
            if (entity == null) return;
            entity.LevelType = LogLevelType.Warn;

            Write(entity);
        }
        public virtual void Error(LogEntity entity)
        {
            if (entity == null) return;
            entity.LevelType = LogLevelType.Error;

            Write(entity);
        }
        public virtual void Fatal(LogEntity entity)
        {
            if (entity == null) return;
            entity.LevelType = LogLevelType.Fatal;

            Write(entity);
        }

        public virtual void Write(LogEntity entity)
        {
            Log.Log(entity);
            //Task.Run(() => );
            //Task.Factory.StartNew(() => Log.Log(entity));
        }
    }
}
