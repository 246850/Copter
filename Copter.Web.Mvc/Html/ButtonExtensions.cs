using System.Web.Mvc;

namespace Copter.Web.Mvc.Html
{
    public static class ButtonExtensions
    {
        public static MvcHtmlString BackButton(this HtmlHelper html)
        {
            return BackButton(html, "javascript:history.go(-1);");
        }

        public static MvcHtmlString BackButton(this HtmlHelper html, string href)
        {
            return BackButton(html, href, "返回");
        }

        public static MvcHtmlString BackButton(this HtmlHelper html, string href, string text)
        {
            return MvcHtmlString.Create(string.Format("<a href='{0}' class='btn btn-flat btn-default'><i class='fa fa-reply'></i>{1}</a>", href, text));
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper html)
        {
            return SubmitButton(html, "保存");
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper html, string text)
        {
            return MvcHtmlString.Create(string.Format("<button type='submit' class='btn btn-flat btn-success'><i class='fa fa-check'></i>{0}</button>", text));
        }

        public static MvcHtmlString SearchButton(this HtmlHelper html)
        {
            return SearchButton(html, "查询");
        }

        public static MvcHtmlString SearchButton(this HtmlHelper html, string text)
        {
            return MvcHtmlString.Create(string.Format("<button type='submit' id='btn-search_0' onclick='gridSearch()' class='btn btn-flat btn-primary'><i class='fa fa-search'></i>{0}</button>", text));
        }

        public static MvcHtmlString AddButton(this HtmlHelper html, string href)
        {
            return AddButton(html, "添加", href);
        }

        public static MvcHtmlString AddButton(this HtmlHelper html, string text, string href)
        {
            return MvcHtmlString.Create(string.Format("<a href='{0}' class='btn btn-flat bg-olive'><i class='fa fa-plus'></i>{1}</a>", href, text));
        }

        public static MvcHtmlString EditButton(this HtmlHelper html, string href)
        {
            return EditButton(html, "编辑", href);
        }

        public static MvcHtmlString EditButton(this HtmlHelper html, string text, string href)
        {
            return MvcHtmlString.Create(string.Format("<a href='{0}' class='btn btn-flat btn-info'><i class='fa fa-edit'></i>{1}</a>", href, text));
        }

        public static MvcHtmlString DeleteButton(this HtmlHelper html)
        {
            return DeleteButton(html, "batchDeleteConfirm()");
        }

        public static MvcHtmlString DeleteButton(this HtmlHelper html, string href)
        {
            return DeleteButton(html, "删除", href);
        }

        public static MvcHtmlString DeleteButton(this HtmlHelper html, string text, string href)
        {
            return MvcHtmlString.Create(string.Format("<button type='button' onclick='{0}' class='btn btn-flat btn-danger'><i class='fa fa-trash'></i>{1}</button>", href, text));
        }

        public static MvcHtmlString ResetButton(this HtmlHelper html)
        {
            return ResetButton(html, "重置");
        }

        public static MvcHtmlString ResetButton(this HtmlHelper html, string text)
        {
            return MvcHtmlString.Create(string.Format("<button type='reset' class='btn btn-flat bg-teal'><i class='fa fa-eraser'></i>{0}</button>", text));
        }
    }
}
