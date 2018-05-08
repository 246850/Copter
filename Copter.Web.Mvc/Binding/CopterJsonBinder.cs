using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;

namespace Copter.Web.Mvc.Binding
{
    /// <summary>
    /// Json字符串 模型绑定
    /// </summary>
    public class CopterJsonBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                string contentType = controllerContext.HttpContext.Request.Headers["content-type"];
                if (Regex.IsMatch(contentType, "application/json", RegexOptions.IgnoreCase))
                {
                    controllerContext.RequestContext.HttpContext.Request.InputStream.Position = 0;
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(bindingContext.ModelType);
                    object result = serializer.ReadObject(controllerContext.RequestContext.HttpContext.Request.InputStream);
                    if (result == null) return base.BindModel(controllerContext, bindingContext);
                    return result;
                }
                return base.BindModel(controllerContext, bindingContext);
            }
            catch
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}
