using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ckan_Front_End.Api.Helper;

namespace Ckan_Front_End.Api.Model
{
    public class Revision
    {
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public List<string> Packages { get; set; }
        public string Id { get; set; }
        public string Author { get; set; }

        public DateTime TimestampAsDate
        {
            get
            {
                return DateHelper.Parse(Timestamp);
            }
        }
    }
}
