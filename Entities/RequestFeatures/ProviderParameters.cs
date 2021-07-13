using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class ProviderParameters : RequestParameters
    {
        public ProviderParameters()
        {
            OrderBy = "name";
        }

        public string SearchTerm { get; set; }
    }
}
