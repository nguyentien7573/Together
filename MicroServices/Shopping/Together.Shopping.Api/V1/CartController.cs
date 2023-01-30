using Microsoft.AspNetCore.Mvc;
using Together.AppContracts.Dtos.Shopping.CartItem;
using Together.Infrastructure.Controller;
using Together.Shopping.Core.UseCases.Commands.CartItems;

namespace Together.Shopping.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class CartController : BaseController
    {
        [HttpPost("/api/v{version:apiVersion}/createCart/")]
        public async Task<ActionResult<CartItemDto>> AddAsync([FromBody] InsertCartItem.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPut("/api/v{version:apiVersion}/updateCart/")]
        public async Task<ActionResult<CartItemDto>> UpdateAsync([FromBody] UpdateCartItem.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpDelete("/api/v{version:apiVersion}/deleteCart")]
        public async Task<ActionResult<CartItemDto>> GetAsync(Guid Id, CancellationToken cancellationToken = new())
        {
            var request = new DeleteCartItem.Command { Id = Id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
