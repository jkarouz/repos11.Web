using System.Web.Optimization;

namespace repos11.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Content/plugins/jquery/jquery.js",
                      "~/Content/plugins/jquery-ui/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                      "~/Content/plugins/bootstrap/js/bootstrap.bundle.js",
                      "~/Content/plugins/moment/moment.js",
                      "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                        "~/Content/vendors/js/adminlte.js"));

            bundles.Add(new ScriptBundle("~/bundles/devextreme").Include(
                        "~/Content/devextreme/js/dx.all.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/site.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/plugins").Include(
                      "~/Content/plugins/fontawesome/css/all.css",
                      "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.css"));

            bundles.Add(new StyleBundle("~/Content/vendors").Include(
                      "~/Content/vendors/css/adminlte.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/devextreme").Include(
                      "~/Content/devextreme/css/dx.light.css"));
        }
    }
}
