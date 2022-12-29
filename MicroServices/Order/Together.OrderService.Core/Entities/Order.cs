using System.Net;
using Together.Core.Domain;

namespace Together.OrderService.Core.Entities
{
    public class Order : EntityBase
    {
        public Guid CustomerId { get; set; }
        public float TotalCost { get; set; }
        public Address Address { get; private set; } = default!;
        public string Status { get; set; } = default!;
        private string Description { get; set; } = default!;
        public List<OrderItem> OrderItems { get; set; } = default!;
    }
}
