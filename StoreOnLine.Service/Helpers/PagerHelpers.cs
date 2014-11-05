using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Service.Helpers
{
    /// <summary>
    /// Ayuda para paguinar 
    /// </summary>
    public static class PagerHelpers
    {
        public static int TotalItemCount = 2000;
        public static int PageSize = 20;
        public static int CurrentPage = 1;

        private static string _page;
        private static string _pagesize;
        private static string _sortby;
        private static string _sortdescending;
        private static string _keyword;
        private static string _location;
        private static string _link = string.Empty;
        private static string _day;
        private static string _price;
        private static string _area;
        private static string _catid;

        public static HtmlString Pager(this HtmlHelper html, string controller, string action, int totalRows, int rowOnPage = 20)
        {
            PageSize = rowOnPage;
            TotalItemCount = totalRows;
            _page = HttpContext.Current.Request.QueryString["page"] ?? "1";
            _pagesize = HttpContext.Current.Request.QueryString["pagesize"] ?? "";
            _sortby = HttpContext.Current.Request.QueryString["sortby"] ?? "";
            _sortdescending = HttpContext.Current.Request.QueryString["sortdescending"] ?? "";
            _keyword = HttpContext.Current.Request.QueryString["keyword"] ?? "";
            _location = HttpContext.Current.Request.QueryString["location"] ?? "";
            _day = HttpContext.Current.Request.QueryString["days"] ?? "";
            _price = HttpContext.Current.Request.QueryString["price"] ?? "";
            _area = HttpContext.Current.Request.QueryString["area"] ?? "";
            _catid = HttpContext.Current.Request.QueryString["catid"] ?? "";

            CurrentPage = Convert.ToInt32(_page);

            if (!string.IsNullOrEmpty(_pagesize))
                PageSize = Convert.ToInt16(_pagesize);

            _pagesize = _pagesize == "" ? "" : "&pagesize=" + _pagesize;
            _keyword = _keyword == "" ? "" : "&keyword=" + _keyword;
            _location = _location == "" ? "" : "&location=" + _location;
            _sortby = _sortby == "" ? "" : "&sortby=" + _sortby;
            _day = _day == "" ? "" : "&days=" + _day;
            _price = _price == "" ? "" : "&price=" + _price;
            _area = _area == "" ? "" : "&area=" + _area;
            _catid = _catid == "" ? "" : "&catid=" + _catid;
            _sortdescending = _sortdescending == "" ? "" : "&sortdescending=" + _sortdescending;
            _link = _pagesize + _keyword + _location + _day + _catid + _area + _price + _sortby + _sortdescending;
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            _link = url + "?page={0}" + _link;
            int startpage = PageSize * (CurrentPage - 1) + 1;
            int endpage = PageSize * CurrentPage;
            if (endpage > totalRows)
                endpage = totalRows;
            return new HtmlString(RenderHtml() + " display " + startpage+ " - " + endpage+ " of " + TotalItemCount);
        }

        public static string RenderHtml()
        {
            var pageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            const int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            // Previous
            sb.Append(CurrentPage > 1 ? GeneratePageLink("&lt;", CurrentPage - 1) : "<span class=\"disabled\">&lt;</span>");

            var start = 1;
            var end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                var middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                var below = (CurrentPage - middle);
                var above = (CurrentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append(GeneratePageLink("2", 2));
                sb.Append("...");
            }

            for (var i = start; i <= end; i++)
            {
                if (i == CurrentPage || (CurrentPage <= 0 && i == 0))
                {
                    sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(CultureInfo.InvariantCulture), i));
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("...");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(CultureInfo.InvariantCulture), pageCount - 1));
                sb.Append(GeneratePageLink(pageCount.ToString(CultureInfo.InvariantCulture), pageCount));
            }

            // Next
            sb.Append(CurrentPage < pageCount ? GeneratePageLink("&gt;", (CurrentPage + 1)) : "<span class=\"disabled\">&gt;</span>");

            return sb.ToString();
        }


        private static string GeneratePageLink(string linkText, int pageNumber)
        {
            var stringBuilder = new StringBuilder("<a");
            stringBuilder.AppendFormat(" href=\"{0}\">{1}</a>", string.Format(_link, pageNumber), linkText);
            return stringBuilder.ToString();
        }
    }
}
