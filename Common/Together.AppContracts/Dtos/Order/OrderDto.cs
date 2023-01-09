namespace Together.AppContracts.Dtos.Order
{
    public class OrderDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public float TotalCost { get; set; }
        public string DistrictCode { get; set; } = default!;
        public string ProvincesCode { get; set; } = default!;
        public string WardCode { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Address1 { get; set; } = default!;
        public string Address2 { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<OrderItemDto> OrderItems { get; set; } = default!;
    }
}
