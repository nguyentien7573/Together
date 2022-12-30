using Microsoft.AspNetCore.Mvc;
using Together.AppContracts.Dtos.Order;
using Together.Infrastructure.Controller;
using Together.OrderService.Core.UseCases.Commands;
using Together.OrderService.Core.UseCases.Queries;

namespace Together.OrderService.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class OrderController : BaseController
    {
        [HttpGet("/api/v{version:apiVersion}/order/{id:guid}")]
        public async Task<ActionResult<OrderDto>> HandleGetOrderByIdAsync(Guid id, CancellationToken cancellationToken = new())
        {
            var request = new GetOrderById.Query { Id = id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/createOrder")]
        public async Task<ActionResult> HandleCreateProductAsync([FromBody] CreateOrder.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
