using Together.Base.Entities;

namespace Together.Products.Core.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
    }
}
