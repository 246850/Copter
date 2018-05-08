using System;
using System.Web;
using System.Web.Mvc;
using Copter.Infrastructure.Configs;
using JWT;
using Copter.Web.Mvc.Principal;

namespace Copter.Web.Mvc.Attribute
{
    /// <summary>
    /// JWT协议标准验证授权 attribute - 默认采用配置文件AppSecret
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MvcJwtAuthorizeAttribute : AuthorizeAttribute
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
            //IList<AllowAnonymousAttribute> attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).OfType<AllowAnonymousAttribute>().ToList();

            try
            {
                IAuthClient<JwtAuthUser<int>, int> authClient = new JwtAuthClient<JwtAuthUser<int>, int>();
                JwtAuthUser<int> authUser = authClient.GetBody();

                if (authUser == null)
                {
                    // cookie不存在
                    filterContext.Result = CreateUnauthorizeResult("未登录授权", filterContext);
                    return;
                }

                CopterIdentity<JwtAuthUser<int> ,int> identity = new CopterIdentity<JwtAuthUser<int>, int>(authUser.Name, authUser);
                CopterPrincipal principal = new CopterPrincipal(identity);
                // 验证成功 赋值 User;
                filterContext.HttpContext.User = principal;
            }
            catch (TokenExpiredException ex)    //  已失效
            {
                filterContext.Result = CreateUnauthorizeResult(ex.Message, filterContext);
            }
            catch (InvalidTokenPartsException ex)   //  Json Web Token 格式错误
            {
                filterContext.Result = CreateUnauthorizeResult(ex.Message, filterContext);
            }
            catch (ArgumentException ex)   //  缺少参数|参数错误
            {
                filterContext.Result = CreateUnauthorizeResult(string.Format("参数错误：{0}", ex.Message), filterContext);
            }
            catch (SignatureVerificationException ex)   //  签名验证对比不通过
            {
                filterContext.Result = CreateUnauthorizeResult(ex.Message, filterContext);
            }
            catch (Exception ex)   //  签名验证对比不通过
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
                //return new CopterJsonResult {Data = errorMsg.ToErrorResult(401, "授权验证不通过，请重新登录授权"), JsonRequestBehavior = JsonRequestBehavior.AllowGet};
                //return new CopterJsonResult { Data = AuthConfigProvider.AuthConfig.LoginUrl.ToUnAuthResult(401, errorMsg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
