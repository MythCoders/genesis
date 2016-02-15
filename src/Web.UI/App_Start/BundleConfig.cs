using System.Web.Optimization;

namespace eSIS.Web.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            #region KendoUI

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        //"~/Scripts/kendo/kendo.grid.min.js",
                        //"~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.default.min.css",
                "~/Content/kendo/kendo.custom.css"));

            #endregion

            #region Admin

            bundles.Add(new StyleBundle("~/Content/cssadmin").Include(
                      "~/Content/aui/aui.min.css",
                      "~/Content/aui/aui-iconfonts.min.css",
                      "~/Content/aui/aui-experimental.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/Scripts/aui/aui.min.js",
                        "~/Scripts/aui/aui-datepicker.min.js",
                        "~/Scripts/aui/aui-experimental.min.js"));

            #endregion

            bundles.IgnoreList.Clear();
        }
    }
}
