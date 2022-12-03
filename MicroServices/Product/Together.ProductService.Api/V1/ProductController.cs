using Microsoft.AspNetCore.Mvc;
using Together.AppContracts.Dtos.Product;
using Together.Infrastructure.Controller;
using Together.ProductService.Core.UseCases.Commands;
using Together.ProductService.Core.UseCases.Queries;

namespace Together.ProductService.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        
        [HttpGet("/api/V1/products/{id:guid}")]
        public async Task<ActionResult<ProductDto>> HandleGetProductByIdAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new GetProductById.Query { Id = id };

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost("/api/V1/products")]
        public async Task<ActionResult> HandleCreateProductAsync([FromBody] CreateProduct.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
