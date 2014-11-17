using System.Web.Optimization;

namespace StoreOnLine
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region JS

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/Scripts/bootstrap.js"));
            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
              "~/Scripts/jquery.unobtrusive*",
              "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/kendojs").Include(
            "~/Scripts/kendo/2014.2.716/kendo.all.min.js",
                // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/2014.2.716/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/SiteJs").Include(
                "~/Scripts/Site/Site.js",
                "~/Scripts/Site/metisMenu.js"));

            #endregion

            #region CSS

            bundles.Add(new StyleBundle("~/bundles/Site").Include(
                "~/Content/Site/Site.css",
                "~/Content/Site/font-awesome.css"));

            bundles.Add(new StyleBundle("~/bundles/Error").Include(
                "~/Content/ErrorStyle.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
              "~/Content/bootstrap.css",
              "~/Content/bootstrap-theme.css"));

            bundles.Add(new StyleBundle("~/bundles/kendocss").Include(
                "~/Content/kendo/2014.2.716/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2014.2.716/kendo.silver.min.css"));
            #endregion

            //bundles.IgnoreList.Clear();
            //BundleTable.EnableOptimizations = true;
        }
    }
}