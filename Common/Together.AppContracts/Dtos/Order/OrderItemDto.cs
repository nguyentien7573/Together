namespace Together.AppContracts.Dtos.Order
{
    public class OrderItemDto : BaseDto
    {
        public Guid ProductId { get; private set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Units { get; set; }
    }
}
