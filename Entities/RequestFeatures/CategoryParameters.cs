using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class CategoryParameters : RequestParameters
    {
        public CategoryParameters()
        {
            OrderBy = "name";
        }

        public string SearchTerm { get; set; }
    }
}
