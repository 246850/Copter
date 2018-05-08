using System;
using System.Collections.Generic;

namespace Copter.Infrastructure.Exceptions
{
    /// <summary>
    /// 自定义标签（即参数）异常
    /// </summary>
    public class CopterTagException: CopterBaseException
    {
        public CopterTagException(string title, IDictionary<string, object> tagList, Exception exception):base(exception.InnerException.Message, exception.InnerException)
        {
            if (tagList == null || exception == null) throw new ArgumentNullException("tagList 或 exception");
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "未定义标题";
            }
            Title = title;
            TagList = tagList;
        }
        /// <summary>
        /// Tag(参数) 字典列表
        /// </summary>
        public IDictionary<string, object> TagList { get; }
    }
}
