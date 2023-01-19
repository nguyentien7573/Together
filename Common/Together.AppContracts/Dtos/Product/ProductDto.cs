namespace Together.AppContracts.Dtos.Product
{
    public class ProductDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public Guid? InventoryId { get; set; }
        public decimal Price { get; set; } = 0;
        public Guid? DiscountId { get; set; }
        public bool IsActive { get; set; }
    }
}
