using Together.Core.Domain;
using Together.Core.Helpers;

namespace Together.ProductService.Core.Entities
{
    public partial class Product : EntityBase
    {
        public string Name { get; private init; } = default!;
        public int Quantity { get; private init; }
        public decimal Cost { get; private init; }

        public static Product Create(string name, int quantity, decimal cost, bool Active)
        {
            Product product = new()
            {
                Name = name,
                Quantity = quantity,
                Cost = cost,
                Active = Active,
            };

            return product;
        }
    }
}
