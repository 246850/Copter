using System;
using System.Collections.Generic;

namespace Copter.Infrastructure.Exceptions
{
    /// <summary>
    /// 异常扩展类
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// tag 异常
        /// </summary>
        public static CopterTagException ToException(this IDictionary<string, object> tagList, string title, Exception ex)
        {
            return new CopterTagException(title, tagList, ex);
        }

        /// <summary>
        /// body 异常
        /// </summary>
        public static CopterBodyException<TBody> ToException<TBody>(this TBody body, string title, Exception ex) where TBody : class, new()
        {
            return new CopterBodyException<TBody>(title, body, ex);
        }
    }
}
