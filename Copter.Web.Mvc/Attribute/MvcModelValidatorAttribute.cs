using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Copter.Result.Json;
using Copter.Web.Mvc.ActionResult;
using Copter.Result;

namespace Copter.Web.Mvc.Attribute
{
    /// <summary>
    /// Mvc FluentValidation 模型验证 过滤器 Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MvcModelValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ModelStateDictionary validator = filterContext.Controller.ViewData.ModelState;

            if (!validator.IsValid)
            {
                List<string> errorList = new List<string>();
                foreach (KeyValuePair<string, ModelState> item in validator)
                {
                    if (item.Value.Errors.Count > 0)
                    {
                        errorList.Add(string.Format("参数{0}，{1}", item.Key, item.Value.Errors.First().ErrorMessage));
                    }
                }
                filterContext.Result = new CopterJsonResult { Data = errorList.ToErrorResult(ResultConst.ModelValidateFailedCode, ResultConst.ModelValidateFailedMessage) };
            }
        }
    }
}
