using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal Cost { get; set; }
    
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product Select(object p)
        {
            throw new NotImplementedException();
        }
    }
}
