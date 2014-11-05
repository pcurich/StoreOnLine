using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace StoreOnLine.Service.Helpers
{
    /// <summary>
    /// Es utilizado para generar Scripts y Css
    /// </summary>
    public static class ScriptHelpers
    {
        public static MvcHtmlString Script(string src)
        {
            var builder = new TagBuilder("script");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("type", "text/javascript");
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString RequireJs(this HtmlHelper helper, string scriptPath)
        {
            var scripts = helper.ViewContext.HttpContext.Items["client-script-list"] as Dictionary<string, string> ?? new Dictionary<string, string>();
            if (!scripts.ContainsKey(scriptPath))
            {
                var sb = new StringBuilder();
                var scriptTag = new TagBuilder("script");
                scriptTag.Attributes.Add("type", "text/javascript");
                scriptTag.Attributes.Add("src", scriptPath);
                sb.AppendLine(scriptTag.ToString());
                scripts.Add(scriptPath, scriptPath);
                helper.ViewContext.HttpContext.Items["client-script-list"] = scripts;
                return new MvcHtmlString(sb.ToString());
            }
            return MvcHtmlString.Empty;
        }
        public static MvcHtmlString RequireCss(this HtmlHelper helper, string scriptPath)
        {
            var scripts = helper.ViewContext.HttpContext.Items["client-script-list"] as Dictionary<string, string> ?? new Dictionary<string, string>();
            if (!scripts.ContainsKey(scriptPath))
            {
                var builder = new TagBuilder("link");
                builder.MergeAttribute("href", scriptPath);
                builder.MergeAttribute("rel", "stylesheet");
                builder.MergeAttribute("type", "text/css");
                scripts.Add(scriptPath, scriptPath);
                helper.ViewContext.HttpContext.Items["client-script-list"] = scripts;
                return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
            }
            return MvcHtmlString.Empty;
        }
        public static MvcHtmlString Style(string src)
        {
            var builder = new TagBuilder("link");
            builder.MergeAttribute("href", src);
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("type", "text/css");
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString Favicon(string src)
        {
            var builder = new TagBuilder("link");
            builder.MergeAttribute("href", src);
            builder.MergeAttribute("rel", "shortcut icon");
            builder.MergeAttribute("type", "image/x-icon");

            //for IE
            var builderIe = new TagBuilder("link");
            builderIe.MergeAttribute("href", src);
            builderIe.MergeAttribute("rel", "icon");
            builderIe.MergeAttribute("type", "image/ico");
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing) + "\n\t" + builderIe.ToString(TagRenderMode.SelfClosing));
        }
    }
}
