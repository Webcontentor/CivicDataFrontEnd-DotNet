using System.Configuration;

namespace CivicDataFrontEnd.Web.Factories
{
    public static class ThemeFactory
    {
        public static string GetCurrentTheme()
        {
            var theme =  ConfigurationManager.AppSettings["Platform.Theme"];
            return string.IsNullOrEmpty(theme) ? "BridgeView" : theme;
        }
    }
}