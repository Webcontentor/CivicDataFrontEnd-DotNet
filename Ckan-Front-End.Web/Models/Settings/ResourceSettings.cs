using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ckan_Front_End.Web.Models.Settings
{
    public class ResourceSettings
    {
        Dictionary<string,ResourceType> types = new Dictionary<string,ResourceType>();

        public Dictionary<string,ResourceType> Types
        {
            get
            {
                return types;
            }
        }
    }
}
