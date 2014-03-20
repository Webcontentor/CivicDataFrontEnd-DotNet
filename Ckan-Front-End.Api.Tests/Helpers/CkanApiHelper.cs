using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ckan_Front_End.Api.Tests.Helpers
{
    public static class CkanApiHelper
    {
        private static string host = "data.opencolorado.org";

        public static CkanClient GetCkanClient()
        {
            return new CkanClient(host);
        }
    } 
}
