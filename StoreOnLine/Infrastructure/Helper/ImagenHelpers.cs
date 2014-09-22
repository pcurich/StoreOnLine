using System.Web.Mvc;
using System.Web.Routing;

namespace StoreOnLine.Infrastructure.Helper
{
    public static class ImagenHelpers
    {
        public static MvcHtmlString Loader(this HtmlHelper html,  object htmlAttributes)
        {
            //        <div id="loading" class="ui-loader-default " style="display: none">
            //    <img src="~/Content/images/ajax-loader.gif" style="max-height: 100%; max-width: 100%;" />
            //</div>

            var builder = new TagBuilder("div");
            builder.AddCssClass("ui-loader-default");
            builder.GenerateId("loader");

            builder.MergeAttribute("src", "~/Content/images/ajax-loader.gif");
            builder.MergeAttribute("alt", "loader");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(builder.ToString());
        }
    }
}