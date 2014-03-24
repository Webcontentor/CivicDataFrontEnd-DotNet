using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CivicDataFrontEnd.Web.Models.SiteMap
{
    public class SiteMapUrl
    {
        public string Location { get; set; }
        public DateTime LastModification { get; set; }
        public SiteMapUrlChangeFrequency ChangeFrequency { get; set; }
        public double Priority { get; set; }
    }
}