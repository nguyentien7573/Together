using Together.Core.Domain;

namespace Together.ProductService.Core.Entities
{
    public partial class Product : EntityBase
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public Guid? InventoryId { get; set; }
        public decimal Price { get; set; } = 0;
        public Guid? DiscountID { get; set; }

        public static Product Create(string name, string description, string sku, Guid categoryId, Guid inventoryId, decimal price, Guid discountID, bool isActive)
        {
            Product product = new()
            {
                Name = name,
                Description = description,
                SKU = sku,
                CategoryId = categoryId,
                InventoryId = inventoryId,
                Price = price, 
                DiscountID = discountID,
                IsActive = isActive
            };

            return product;
        }
    }
}
