using Together.Core.Domain;

namespace Together.OrderService.Core.Entities
{
    public class OrderItem : EntityBase
    {
        public int ProductId { get; private set; }
        private decimal UnitPrice { get; set; }
        private decimal Discount { get; set; }
        private int Units { get; set; }
    }
}
