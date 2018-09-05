using System.Web;
using System.Web.Optimization;

namespace HotelPanel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-3.3.1.min.js",
                      "~/Scripts/jquery-ui-1.12.1.min.js",
                      "~/Content/Scripts/vendor.js",
                      "~/Content/Scripts/bundle.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                      "~/Content/plugin/ckeditor/styles.js",
                      "~/Content/plugin/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/owlcarousel").Include(
                      "~/Scripts/owl.carousel.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Style/Style.css",
                "~/Content/Style/StyleSheet1.css",
                "~/Content/themes/base/jquery-ui.min.css",
                "~/Content/Style/owl.carousel.min.css"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
