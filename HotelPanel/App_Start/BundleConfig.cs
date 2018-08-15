using System.Web;
using System.Web.Optimization;

namespace HotelPanel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Content/Scripts").Include(
                      "~/Scripts/vendor.js",
                      "~/Scripts/bundle.js"));

            bundles.Add(new StyleBundle("~/Content/Style").Include(
                      "~/Content/style.css"));
        }
    }
}
