using System.Net;
using Together.Core.Domain;

namespace Together.OrderService.Core.Entities
{
    public class Order : EntityBase
    {
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public Guid PaymentId { get; set; }
        public string DistrictCode { get; set; } = default!;
        public string ProvincesCode { get; set; } = default!;
        public string WardCode { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Address1 { get; set; } = default!;
        public string Address2 { get; set; } = default!;
        public List<OrderItem> OrderItems { get; set; } = default!;

        public static Order Create(Guid customerId, decimal total, string address, string address1,
            string address2, string districtCode, string provincesCode, string wardCode, List<OrderItem> orderItems)
        {
            Order order = new()
            {
                CustomerId = customerId,
                Total = total,
                Address = address,
                Address1 = address1,
                Address2 = address2,
                DistrictCode = districtCode,
                ProvincesCode = provincesCode,
                WardCode = wardCode,
                OrderItems = orderItems
            };

            return order;
        }
    }
}
