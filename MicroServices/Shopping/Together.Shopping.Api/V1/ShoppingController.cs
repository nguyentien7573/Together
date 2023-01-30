using Microsoft.AspNetCore.Mvc;
using Together.AppContracts.Dtos.Shopping.Shopping;
using Together.Infrastructure.Controller;
using Together.Shopping.Core.UseCases.Commands.ShoppingSessions;

namespace Together.Shopping.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ShoppingController : BaseController
    {
        [HttpPost("/api/v{version:apiVersion}/createShoppingSession/")]
        public async Task<ActionResult<ShoppingSessionDto>> AddAsync([FromBody] InsertShoppingSession.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPut("/api/v{version:apiVersion}/updateShoppingSession/")]
        public async Task<ActionResult<ShoppingSessionDto>> UpdateAsync([FromBody] UpdateShoppingSessions.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpDelete("/api/v{version:apiVersion}/deleteShoppingSession")]
        public async Task<ActionResult<ShoppingSessionDto>> GetAsync(Guid Id, CancellationToken cancellationToken = new())
        {
            var request = new DeleteShoppingSessions.Command { Id = Id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
