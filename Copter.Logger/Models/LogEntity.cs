using System;
using System.Collections.Generic;
using System.Configuration;
using Copter.Infrastructure.Utils;

namespace Copter.Logger.Models
{
    /// <summary>
    /// 日志 类
    /// </summary>
    public class LogEntity
    {
        public LogEntity()
        {
            string appId = ConfigurationManager.AppSettings["AppId"];
            if (!string.IsNullOrWhiteSpace(appId))
            {
                AppId = appId;

            }
            CreateTime = DateTime.Now;
            Ip = MiscUtil.GetRemoteIp();
        }
        /// <summary>
        /// 所属系统Id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 堆栈
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// Action名
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 附加数据 列表
        /// </summary>
        public IList<LogTagEntity> Tags { get; set; }
        /// <summary>
        /// 日志类型|级别
        /// </summary>
        public LogLevelType LevelType { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
