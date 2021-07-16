﻿using System.Collections.Generic;

namespace Entities.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
