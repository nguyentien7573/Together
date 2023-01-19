using Together.Core.Domain;

namespace Together.Shopping.Core.Entities
{
    public class CartItem : EntityBase
    {
        public Guid SessionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public static CartItem Create(Guid sessionId, Guid productId, int quantity)
        {
            return new() { SessionId = sessionId, ProductId = productId, Quantity = quantity};
        }
        
    }
}
