using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public ProductParameters()
        {
            OrderBy = "name";
        }

        public string SearchTerm { get; set; }

        public string Fields { get; set; }
        
        public string Currency { get; set; }

        public string Categories { get; set; }

        public string Providers { get; set; }

        public decimal MinCost { get; set; }

        public decimal MaxCost { get; set; } = decimal.MaxValue;
    }
}
