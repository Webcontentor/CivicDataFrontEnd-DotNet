using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ckan_Front_End.Web.Models.Helpers;
using Ckan_Front_End.Web.Models;

namespace Ckan_Front_End.Web.Controllers
{
    public class SiteMapController : Controller
    {
        //
        // GET: /SiteMap/
        public ActionResult Index()
        {
            Response.ContentType = "text/xml";
            var urlSet = SiteMapHelper.GetSiteMapUrls(this.ControllerContext);
            return View(urlSet);
        }

    }
}
