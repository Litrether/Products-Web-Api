﻿using System.Collections.Generic;

namespace Entities.Models
{
    public class Provider
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal LocationLong { get; set; }

        public decimal LocationLat { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
