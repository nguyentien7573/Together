using Together.Core.Domain;

namespace Together.Shopping.Core.Entities
{
    public class ShoppingSession : EntityBase
    {
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static ShoppingSession Create(Guid customerId, decimal total, List<CartItem> cartItems)
        {
            return new() { CartItems = cartItems, CustomerId = customerId, Total = total };

        }
    }
}
