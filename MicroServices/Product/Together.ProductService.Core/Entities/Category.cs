﻿using Together.Core.Domain;

namespace Together.ProductService.Core.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {
            Product = new List<Product>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }

        public static Category Create(string name, string description, bool Active)
        {
            Category category = new()
            {
                Name = name,
                Description = description,
                IsActive = Active,
            };

            return category;
        }
    }
}
