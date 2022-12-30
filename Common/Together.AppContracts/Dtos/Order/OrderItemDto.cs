namespace Together.AppContracts.Dtos.Order
{
    public class OrderItemDto
    {
        public int ProductId { get; private set; }
        private decimal UnitPrice { get; set; }
        private decimal Discount { get; set; }
        private int Units { get; set; }
    }
}
