using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using StoreOnLine.Domain.Base;

namespace StoreOnLine.Domain.HtmlHelpers
{
    public static class Accordion
    {
        public static MvcHtmlString CreateAccordion(this HtmlHelper html,BoostrapCss sizeOfFirstRow)
        {
            var result = new StringBuilder();

            var container = new TagBuilder(HtmlTextWriterTag.Div.ToString());
            container.AddCssClass("container");

            var row = new TagBuilder(HtmlTextWriterTag.Div.ToString());
            row.AddCssClass("row");
            
            var col = new TagBuilder(HtmlTextWriterTag.Div.ToString());
            col.AddCssClass(StringEnum.GetStringValue(sizeOfFirstRow));

            var panelGroup =new  TagBuilder(HtmlTextWriterTag.Div.ToString());
            panelGroup.AddCssClass("panel panel-group");
            panelGroup.IdAttributeDotReplacement = "main";

            var panelBody = new TagBuilder(HtmlTextWriterTag.Div.ToString());
            panelBody.AddCssClass("panel-body");

            var panelHead = new TagBuilder(HtmlTextWriterTag.Div.ToString());
            panelHead.AddCssClass("panel-heading");

            var h4 = new TagBuilder(HtmlTextWriterTag.H4.ToString());

            panelBody.InnerHtml = panelHead.ToString();
            panelGroup.InnerHtml = panelBody.ToString();
            col.InnerHtml = panelGroup.ToString();
            row.InnerHtml = col.ToString();
            container.InnerHtml = row.ToString();

            return MvcHtmlString.Create(result.ToString());
        }
    }

    public enum BoostrapCss
    {
        
    }

}
