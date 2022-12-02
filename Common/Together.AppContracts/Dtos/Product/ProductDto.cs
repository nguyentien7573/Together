namespace Together.AppContracts.Dtos.Product
{
    public class ProductDto
    {
        public string Name { get; set; } = default!;
        public bool Active { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public Guid ProductCodeId { get; set; }
      
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
