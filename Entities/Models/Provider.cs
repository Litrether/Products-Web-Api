using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Provider
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  ICollection<Product> Products { get; set; }
    }
}
