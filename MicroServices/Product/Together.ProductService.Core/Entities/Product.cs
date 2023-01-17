using Together.Core.Domain;

namespace Together.ProductService.Core.Entities
{
    public partial class Product : EntityBase
    {
        public string Name { get; private init; } = default!;
        public int Quantity { get; private init; }
        public decimal Cost { get; private init; }
        public Guid CategoryId { get; set; }

        public static Product Create(string name, int quantity, decimal cost, Guid categoryId, bool Active)
        {
            Product product = new()
            {
                Name = name,
                Quantity = quantity,
                Cost = cost,
                CategoryId = categoryId,
                Active = Active,
            };

            return product;
        }
    }
}
