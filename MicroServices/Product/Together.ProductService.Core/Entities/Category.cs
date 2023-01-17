using Together.Core.Domain;

namespace Together.ProductService.Core.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {
            Product = new List<Product>();
        }

        public string Name { get; set; }
        public virtual ICollection<Product> Product { get; set; }

        public static Category Create(string name ,bool Active)
        {
            Category category = new()
            {
                Name = name,
                Active = Active,
            };

            return category;
        }
    }
}
