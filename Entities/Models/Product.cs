using System.Collections.Generic;

namespace Entities.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public string ImageUrl { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual List<Cart> Carts { get; set; }
    }
}
