using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Copter.Infrastructure.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Copter.Web.Mvc.ActionResult
{
    /// <summary>
    /// JsonResult 重写， 采用Json.Net序列化
    /// </summary>
    public class CopterJsonResult : JsonResult
    {
        /// <summary>
        /// 日期格式字符串
        /// </summary>
        public string DateTimeFormattingString { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.RequestContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("不允许Get请求方法");

            HttpResponseBase response = context.RequestContext.HttpContext.Response;
            response.ContentType = string.IsNullOrWhiteSpace(ContentType) ? "application/json" : ContentType;
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;
            HttpRequestBase request = context.RequestContext.HttpContext.Request;
            string callback = request.QueryString["callback"];
            Formatting formatting;
#if DEBUG
            formatting = Formatting.Indented;
#else
            formatting = Formatting.None;
#endif
            IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter
            {
                Culture = CultureInfo.InvariantCulture,
                DateTimeFormat = string.IsNullOrWhiteSpace(DateTimeFormattingString) ? "yyyy-MM-dd HH:mm:ss" : DateTimeFormattingString
            };
            response.Write(string.IsNullOrWhiteSpace(callback) ? Data.SerializeObject(formatting, dateTimeConverter) : string.Format("{0}({1})", callback, Data.SerializeObject(formatting, dateTimeConverter)));
        }
    }
}
