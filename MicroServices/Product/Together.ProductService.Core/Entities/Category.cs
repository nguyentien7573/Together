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
    }
}
