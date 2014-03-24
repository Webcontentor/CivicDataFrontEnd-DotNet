using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CivicDataFrontEnd.Api.Model
{
    public class ResourceSearchResponse<T>
    {
        public int Count { get; set; }
        public List<T> Results { get; set; }

        public ResourceSearchResponse()
        {
            Results = new List<T>();
        }
    }
}
