﻿using System;
using System.Web.Mvc;

namespace StoreOnLine.Infrastructure.Helper
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            var tag = new TagBuilder("ul");
            foreach (var str in list)
            {
                var itemTag = new TagBuilder("li");
                itemTag.SetInnerText(str);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg)
        {
            var encodeMessage = html.Encode(msg);
            var result = String.Format("This is the message: <p>{0}</p>", encodeMessage);
            return new MvcHtmlString(result);
        }

        public static string DisplayMessage1(this HtmlHelper html, string msg1)
        {
            return String.Format("This is the message: <p>{0}</p>", msg1);
        }

        public static MvcHtmlString DisplayProgressBar(this HtmlHelper html, string msg)
        {
            var tagProgress = new TagBuilder("div");
            tagProgress.AddCssClass("progress");
            var tagBar = new TagBuilder("div");
            tagBar.MergeAttribute("role", "progressbar");
            tagBar.MergeAttribute("aria-valuenow", "80");
            tagBar.MergeAttribute("aria-valuemin", "0");
            tagBar.MergeAttribute("aria-valuemax", "100");
            tagBar.MergeAttribute("style", "width: 80%");
            tagBar.MergeAttribute("role", "progressbar");
            tagBar.AddCssClass("progress-bar progress-bar-danger");

            var tagSpan = new TagBuilder("span");
            tagSpan.AddCssClass("sr-only");
            tagSpan.SetInnerText("80% completado (peligro / error)");

            tagBar.InnerHtml = tagSpan.ToString();

            tagProgress.InnerHtml = tagBar.ToString();

            return new MvcHtmlString(tagProgress.ToString()); ;

            //<div class="progress">
            //  <div class="progress-bar progress-bar-danger" role="progressbar"
            //       aria-valuenow="80" aria-valuemin="0" aria-valuemax="100"
            //       style="width: 80%">
            //    <span class="sr-only">80% completado (peligro / error)</span>
            //  </div>
            //</div>
        }
    }
}