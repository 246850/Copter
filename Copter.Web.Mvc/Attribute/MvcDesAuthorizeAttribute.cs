using System;
using System.Web;
using System.Web.Mvc;
using Copter.Infrastructure.Configs;
using Copter.Web.Mvc.Principal;

namespace Copter.Web.Mvc.Attribute
{
    /// <summary>
    /// Des加解密验证授权 attribute - 默认采用配置文件AppKey,AppSecret作为DesIv,DesKey
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MvcDesAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            // 判断Action或Controller是否存在AllowAnonymousAttribute 特性 - 匿名访问
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;

            try
            {
                IAuthClient<DesAuthUser<int>, int> authClient = new DesAuthClient<DesAuthUser<int>, int>();
                DesAuthUser<int> authUser = authClient.GetBody();
                if (authUser == null)
                {
                    // cookie不存在
                    filterContext.Result = CreateUnauthorizeResult("未登录授权", filterContext);
                    return;
                }
                CopterIdentity<DesAuthUser<int>, int> identity = new CopterIdentity<DesAuthUser<int>, int>(authUser.Name, authUser);
                CopterPrincipal principal = new CopterPrincipal(identity);
                // 验证成功 赋值 User;
                filterContext.HttpContext.User = principal;
            }
            catch (Exception ex)
            {
                filterContext.Result = CreateUnauthorizeResult(ex.Message, filterContext);
            }
        }

        /// <summary>
        /// 创建授权验证不通过 返回Json结果
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="filterContext">授权上下文</param>
        /// <returns>CopterJsonResult对象</returns>
        System.Web.Mvc.ActionResult CreateUnauthorizeResult(string errorMsg, AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest()) // Ajax方式 返回Json结果
            {
                return new HttpUnauthorizedResult(errorMsg);
            }
            return new ContentResult
            {
                ContentType = "text/html",
                Content = string.Format("<script type='text/javascript'>alert('{1}');window.top.location.href='{0}';</script>", AuthConfigProvider.AuthConfig.LoginUrl, errorMsg)
            };
        }
    }
}
