﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ckan_Front_End.Api.Model;

namespace Ckan_Front_End.Web.Models
{
    public class PackageSearchResultsModel : SearchResultsSideBarModel
    {
        public PackageSearchResponse<Package> SearchResults { get; set; }
        public Pager Pager { get; set; }
        public PackageSearchParameters SearchParameters { get; set; }
        public ResultsDisplayMode DisplayMode { get; set; }
    }
}