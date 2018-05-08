using System.Text;
using System.Web.Mvc;
using Copter.Web.Mvc.ActionResult;
using Copter.Web.Mvc.Principal;

namespace Copter.Web.Mvc.Controllers
{
    /// <summary>
    /// Mvc Controller 基类 使用Json.Net序列化 写
    /// </summary>
    public abstract class CopterController : Controller
    {
        protected JwtAuthUser<int> AuthUser
        {
            get
            {
                CopterPrincipal principal = User as CopterPrincipal;
                CheckNotNull(principal, "principal");
                CopterIdentity<JwtAuthUser<int>, int> identity = principal.Identity as CopterIdentity<JwtAuthUser<int>, int>;
                CheckNotNull(identity, "identity");
                return identity.Body;
            }
        }

        void CheckNotNull(object obj, string paramName)
        {
            if (obj == null) throw new System.ArgumentNullException(paramName);
        }

        protected JsonResult JsonWithTimeConverter(object data, string dateTimeConverter, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return new CopterJsonResult
            {
                Data = data,
                DateTimeFormattingString = dateTimeConverter,
                JsonRequestBehavior = behavior
            };
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CopterJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}
