using System;
using System.Web.Mvc;

namespace Copter.Web.Mvc.Attribute
{
    /// <summary>
    /// 全局 异常 处理 过滤器
    /// </summary>
    public class MvcExceptionFilterAttribute: FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}
