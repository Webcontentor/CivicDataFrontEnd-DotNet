﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ckan_Front_End.Api.Model
{
    public class PackageSearchResponse<T>
    {
        public int Count { get; set; }
        public List<T> Results { get; set; }
        public List<Tag> Tags { get; set; }

        public PackageSearchResponse()
        {
            Results = new List<T>();
            Tags = new List<Tag>();
        }
    }
}
