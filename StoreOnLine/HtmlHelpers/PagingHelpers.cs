using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using StoreOnLine.Areas.Management.Models;

namespace StoreOnLine.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary ");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}