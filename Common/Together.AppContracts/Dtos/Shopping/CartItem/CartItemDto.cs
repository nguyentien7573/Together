using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Together.AppContracts.Dtos.Shopping.CartItem
{
    public class CartItemDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
