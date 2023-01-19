using Together.Core.Domain;

namespace Together.OrderService.Core.Entities
{
    public class OrderItem : EntityBase
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
