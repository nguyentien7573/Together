using Together.AppContracts.Dtos.Shopping.CartItem;

namespace Together.AppContracts.Dtos.Shopping.Shopping
{
    public class ShoppingSessionDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
