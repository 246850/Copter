using System;
using System.Collections.Generic;

namespace Copter.Infrastructure.Exceptions
{
    /// <summary>
    /// 异常 Tags 构建类
    /// </summary>
    public sealed class ExceptionTagBuilder
    {
        private IDictionary<string, object> tagList;
        private ExceptionTagBuilder() { tagList = new Dictionary<string, object>(); }

        public static ExceptionTagBuilder Instance { get { return new ExceptionTagBuilder(); } }
        public ExceptionTagBuilder Append(string param, object value)
        {
            tagList[param] = value;
            return this;
        }
        public CopterTagException ToException(string title, Exception ex)
        {
            return tagList.ToException(title, ex);
        }

    }
}
