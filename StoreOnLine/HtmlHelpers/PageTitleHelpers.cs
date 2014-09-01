namespace StoreOnLine.HtmlHelpers
{
    public class PageTitleHelpers
    {
        public string Big { get; set; }
        public string Small { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }

        public PageTitleHelpers(string big="", string small = "", string area = "", string controller = "", string view = "")
        {
            Big = big;
            Small = small;
            Area = area;
            Controller = controller;
            View = view;
        }
    }
}