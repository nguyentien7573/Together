namespace Together.AppContracts.Dtos.Order
{
    public class OrderItemDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
