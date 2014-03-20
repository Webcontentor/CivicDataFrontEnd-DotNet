using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ckan_Front_End.Api.Exceptions
{
    public class CkanException : Exception
    {
        public CkanException(string message)
            : base(message)
        {
        }

        public CkanException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}
