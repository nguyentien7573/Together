using MediatR;
using Microsoft.AspNetCore.Mvc;
using Together.Products.Application.Commands;
using Together.Products.Application.Responses;

namespace Together.Proudcts.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductResponse>> CreateMovie([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
