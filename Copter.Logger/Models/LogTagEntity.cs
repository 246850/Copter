using System;

namespace Copter.Logger.Models
{
    /// <summary>
    /// 日志 附加数据 类
    /// </summary>
    public class LogTagEntity
    {
        public LogTagEntity()
        {
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 键名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值内容
        /// </summary>
        public object Value { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
