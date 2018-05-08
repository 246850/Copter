using System;

namespace Copter.Infrastructure.Exceptions
{
    /// <summary>
    /// 包含数据载体的异常
    /// </summary>
    public class CopterBodyException<TBody> : CopterBaseException where TBody : class, new()
    {
        public CopterBodyException(string title, TBody body, Exception exception) : base(exception.InnerException.Message, exception.InnerException)
        {
            if (body == null || exception == null) throw new ArgumentNullException("body 或 exception");
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "未定义标题";
            }
            Title = title;
            Body = body;
        }
        public TBody Body { get; }
    }
}
