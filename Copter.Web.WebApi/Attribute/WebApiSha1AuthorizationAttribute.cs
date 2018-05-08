using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Copter.Infrastructure.Security;
using Copter.Infrastructure.Serialization;
using Copter.Infrastructure.ValueObject;
using Copter.Result.Json;
using Copter.Infrastructure.Models;

namespace Copter.Web.WebApi.Attribute
{
    /// <summary>
    /// WebApi 接口 安全验证 - 使用Sha1基于AppKey & AppSecret加密
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class WebApiSha1AuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            // 判断是否存在AllowAnonymousAttribute 特性
            IList<AllowAnonymousAttribute> attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
            if (attributes != null && attributes.Count > 0) return;

            // 开始验证授权
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = CreateUnauthorizedMessage("缺失授权请求头");
                return;
            }

            string scheme = actionContext.Request.Headers.Authorization.Scheme,
                parameter = actionContext.Request.Headers.Authorization.Parameter;  // base64编码
            if (!string.Equals(scheme, "Bearer", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(parameter))
            {
                actionContext.Response = CreateUnauthorizedMessage("授权请求头格式错误！");
                return;
            }
            AuthParameterModel model;
            try
            {
                // base64 解码 并序列化成对象
                model = parameter.DeBase64().DeserializeObject<AuthParameterModel>();
            }
            catch
            {
                actionContext.Response = CreateUnauthorizedMessage("授权参数格式错误！");
                return;
            }
            if (model == null || string.IsNullOrWhiteSpace(model.AppKey) || string.IsNullOrWhiteSpace(model.Nonce) || string.IsNullOrWhiteSpace(model.Signature))
            {
                actionContext.Response = CreateUnauthorizedMessage("缺少授权参数！");
                return;
            }
            DateTime expires = model.Timestamp.AsDateTime(),
                beginTime = DateTime.Now.AddMinutes(-10),
                endTime = DateTime.Now.AddMinutes(10);
            if (expires < beginTime || expires > endTime)
            {
                actionContext.Response = CreateUnauthorizedMessage("授权参数已失效！");
                return;
            }
            string internalAppSecret = GetAppSecret(model.AppKey);
            if (string.IsNullOrWhiteSpace(internalAppSecret))
            {
                actionContext.Response = CreateUnauthorizedMessage(string.Format("不存在应用标识AppKey为：{0}的应用", model.AppKey));
                return;
            }
            // Sh1加密
            List<string> list = new List<string>()
            {
                internalAppSecret,
                model.Timestamp.ToString(),
                model.Nonce
            };
            // 字典排序
            list.Sort();
            ICryptor cryptor = new Sha1Cryptor();
            string internallSignature = cryptor.Encrypt(string.Join(string.Empty, list));
            if (!string.Equals(model.Signature, internallSignature, StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = CreateUnauthorizedMessage("授权签名不正确");
            }
        }

        HttpResponseMessage CreateUnauthorizedMessage(string error)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(error.ToErrorResult(401, "授权验证不通过").SerializeObject(), Encoding.UTF8, "application/json")
            };
        }

        /// <summary>
        /// 获取应用程序 密钥
        /// </summary>
        /// <param name="appKey">应用程序唯一标识 key</param>
        /// <returns>密钥</returns>
        string GetAppSecret(string appKey)
        {
            if (string.Equals(appKey, DefaultSecretConst.AppKey, StringComparison.OrdinalIgnoreCase))
            {
                return DefaultSecretConst.AppSecret;
            }
            return string.Empty;
        }
    }
}
