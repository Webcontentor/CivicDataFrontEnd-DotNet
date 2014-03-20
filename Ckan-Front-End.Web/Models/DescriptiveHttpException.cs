using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ckan_Front_End.Web.Models
{
    public class DescriptiveHttpException : HttpException
    {
        public string Description { get; set; }

        public DescriptiveHttpException(int httpCode, string message, string description)
            : base(httpCode, message)
        {
            Description = description;
        }
    }
}