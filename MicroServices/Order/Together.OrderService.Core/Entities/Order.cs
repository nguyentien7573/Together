using System.Net;
using Together.Core.Domain;

namespace Together.OrderService.Core.Entities
{
    public class Order : EntityBase
    {
        public Guid CustomerId { get; set; }
        public float TotalCost { get; set; }
        public string Status { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DistrictCode { get; set; } = default!;
        public string ProvincesCode { get; set; } = default!;
        public string WardCode { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Address1 { get; set; } = default!;
        public string Address2 { get; set; } = default!;

        public List<OrderItem> OrderItems { get; set; } = default!;

        public static Order Create(Guid customerId, float totalCost, string address, string address1, 
            string address2, string districtCode, string provincesCode, string wardCode,
            string status, string description, List<OrderItem> orderItems)
        {
            Order order = new()
            {
                CustomerId = customerId,
                TotalCost = totalCost,
                Address = address,
                Address1 = address1,
                Address2= address2,
                DistrictCode = districtCode,
                ProvincesCode = provincesCode,
                WardCode = wardCode,
                Status = status,
                Description = description,
                OrderItems = orderItems
               
            };

            return order;
        }
    }
}
