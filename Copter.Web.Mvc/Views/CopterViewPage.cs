using Copter.Web.Mvc.Principal;
using System.Text;
using System.Web.Mvc;

namespace Copter.Web.Mvc.Views
{
    /// <summary>
    /// 抽象WebViewPage  基类
    /// </summary>
    /// <typeparam name="TModel">业务模型</typeparam>
    public abstract class CopterViewPage<TModel> : WebViewPage<TModel> where TModel : class, new()
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

        /// <summary>
        /// kendo grid 分页 参数
        /// </summary>
        protected string Pageable
        {
            get { return "{input: true, numeric: true, refresh: true, pageSizes: [10, 20, 30, 50, 100], pageSize: 10}"; }
        }

        /// <summary>
        /// 输出分页
        /// </summary>
        /// <param name="totalPage">总页数</param>
        /// <param name="page">当前页</param>
        /// <returns></returns>
        protected MvcHtmlString RenderPager(long totalPage, int page)
        {
            if (totalPage <= 0) return MvcHtmlString.Empty;
            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append("<ul class='am-pagination'>");
            int start = page - 5;
            if (start < 1)
            {
                start = 1;
            }
            long end = start + 9;
            if (end > totalPage)
            {
                end = totalPage;
            }
            if (start != end)
            {
                if (start <= 1)
                {
                    stringBuilder.Append("<li class='am-disabled'><a href='javascript:void(0);'>&laquo;</a></li>");
                }
                else
                {
                    stringBuilder.AppendFormat("<li><a href='javascript:doQuery({0})'>&laquo;</a></li>", start - 1);
                }

                for (int i = start; i <= end; i++)
                {
                    string className = i == page ? "am-active" : string.Empty;
                    stringBuilder.AppendFormat("<li class='{0}'><a href='javascript:doQuery({1});' title='{2}'>{1}</a></li>", className, i, string.Format("第{0}页", i));
                }

                if (end >= totalPage)
                {
                    stringBuilder.Append("<li class='am-disabled'><a href='javascript:void(0);'>&raquo;</a></li>");
                }
                else
                {
                    stringBuilder.AppendFormat("<li><a href='javascript:doQuery({0})'>&raquo;</a></li>", end + 1);
                }
            }

            stringBuilder.Append("</ul>");

            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}
