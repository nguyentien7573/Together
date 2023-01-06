using Together.Core.Domain;

namespace Together.OrderService.Core.Entities
{
    public class OrderItem : EntityBase
    {
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Units { get; set; }
    }
}
