using System;

namespace Copter.Infrastructure.ValueObject
{
    /// <summary>
    /// 时间日期 常量类
    /// </summary>
    public sealed class CopterDateTime
    {
        /// <summary>
        /// DateTime 1970年1月1日0时0分0秒
        /// </summary>
        public static DateTime DateTime1970 => TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
    }
}
