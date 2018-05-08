using System.Web.Http.Filters;

namespace Copter.Web.WebApi.Attribute
{
    /// <summary>
    /// WebApi全局异常过滤器
    /// </summary>
    public class WebApiExceptionFilterAtrribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}
