using System;
using System.Collections.Generic;
using Copter.Logger.Models;

namespace Copter.Logger
{
    /// <summary>
    /// 日志扩展类
    /// </summary>
    public static class LogExtensions
    {
        public static LogEntity ToLogEntity(this Exception ex, string title)
        {
            return ToLogEntity(ex, title, null);
        }
        public static LogEntity ToLogEntity(this Exception ex, string title, IList<LogTagEntity> tags)
        {
            return ToLogEntity(ex, title, null, null, tags);
        }
        public static LogEntity ToLogEntity(this Exception ex, string title, string controller, string action)
        {
            return ToLogEntity(ex, title, controller, action, null);
        }
        public static LogEntity ToLogEntity(this Exception ex, string title, string controller, string action, IList<LogTagEntity> tags)
        {
            return new LogEntity
            {
                Title = title,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                ControllerName = controller,
                ActionName = action,
                Tags = tags
            };
        }
    }
}
