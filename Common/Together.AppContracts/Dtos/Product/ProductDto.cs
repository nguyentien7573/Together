namespace Together.AppContracts.Dtos.Product
{
    public class ProductDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Active { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public Guid CategoryId { get; set; }
    }
}
