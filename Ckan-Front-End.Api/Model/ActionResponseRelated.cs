using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ckan_Front_End.Api.Model
{
    public class ActionResponseRelated : ActionResponse
    {
        /// <summary>
        /// Gets the related items
        /// </summary>
        public List<Related> Result { get; set; }
    }
}
