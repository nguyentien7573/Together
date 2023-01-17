using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Together.AppContracts.Dtos.Product;
using Together.Infrastructure.Controller;
using Together.ProductService.Core.UseCases.Commands.Products;
using Together.ProductService.Core.UseCases.Queries;

namespace Together.ProductService.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        [HttpGet("/api/v{version:apiVersion}/product/{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = new())
        {
            var request = new GetProductById.Query { Id = id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/createProduct")]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProduct.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
