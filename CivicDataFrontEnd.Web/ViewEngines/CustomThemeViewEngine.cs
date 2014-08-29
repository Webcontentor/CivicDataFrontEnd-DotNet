﻿using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CivicDataFrontEnd.Web.ViewEngines
{
    public class CustomThemeViewEngine : RazorViewEngine
    {
        private readonly string _themeName;

        public CustomThemeViewEngine()
        {
            var theme = ConfigurationManager.AppSettings["Platform.Theme"];
            _themeName = string.IsNullOrEmpty(theme) ? "BridgeView" : theme;
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var customThemePartialViewName = DetermineCustomThemePartialViewFileName(partialViewName);
            var doesCustomThemePartialViewExist = File.Exists(HttpContext.Current.Server.MapPath(customThemePartialViewName));
            var partialViewNameToUse = doesCustomThemePartialViewExist ? customThemePartialViewName : partialViewName;
            return base.FindPartialView(controllerContext, partialViewNameToUse, useCache);
        }

        private string DetermineCustomThemePartialViewFileName(string partialViewName)
        {
            const string themeFolder = "/theme";
            var caseInsensitiveViewName = partialViewName.ToLower();

            var isThemedPartialView = caseInsensitiveViewName.Contains(themeFolder);

            if (!isThemedPartialView) return partialViewName;

            return caseInsensitiveViewName.Replace(themeFolder, themeFolder + "/" + _themeName);
        }
    }
}