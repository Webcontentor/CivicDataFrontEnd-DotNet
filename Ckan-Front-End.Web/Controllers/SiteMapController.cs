using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CivicDataFrontEnd.Web.Models.Helpers;
using CivicDataFrontEnd.Web.Models;

namespace CivicDataFrontEnd.Web.Controllers
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
