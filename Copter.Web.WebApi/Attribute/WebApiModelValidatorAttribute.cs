using Copter.Result;
using Copter.Result.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;


namespace Copter.Web.WebApi.Attribute
{
    /// <summary>
    /// WebApi FluentValidation 模型验证 过滤器 Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class WebApiModelValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                List<string> errorList = new List<string>();
                foreach (KeyValuePair<string, ModelState> item in actionContext.ModelState)
                {
                    errorList.Add(string.Format("参数{0}，{1}", item.Key, item.Value.Errors.First().ErrorMessage));
                }

                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(errorList.ToErrorResult(ResultConst.ModelValidateFailedCode, ResultConst.ModelValidateFailedMessage)), System.Text.Encoding.UTF8, "application/json")
                };
                actionContext.Response = response;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
