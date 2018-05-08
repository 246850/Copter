using System;

namespace Copter.Infrastructure.Exceptions
{
    /// <summary>
    /// 自定义异常 基类
    /// </summary>
    public abstract class CopterBaseException :Exception
    {
        public CopterBaseException(string message, Exception innerException):base(message, innerException) { }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
    }
}
