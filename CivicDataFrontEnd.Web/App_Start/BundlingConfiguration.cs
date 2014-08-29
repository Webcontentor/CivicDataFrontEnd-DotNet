using System.Web.Optimization;
using CivicDataFrontEnd.Web.Factories;


namespace CivicDataFrontEnd.Web.App_Start
{
    public class BundlingConfiguration
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            var themeName = ThemeFactory.GetCurrentTheme();
            bundles.Add(new StyleBundle("~/styles/current-theme").Include("~/content/theme/" + themeName + "/*.css"));
        }
    }
}