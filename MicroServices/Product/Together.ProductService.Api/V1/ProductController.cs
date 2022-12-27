using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Together.AppContracts.Dtos.Product;
using Together.Infrastructure.Controller;
using Together.Product.Process.Interface;
using Together.ProductService.Core.UseCases.Commands;
using Together.ProductService.Core.UseCases.Queries;

namespace Together.ProductService.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        private readonly IRabitMQProducer _rabitMQProducer;
        public ProductController(IRabitMQProducer rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }

        [Authorize(Roles ="member")]
        [HttpGet("/api/v{version:apiVersion}/products/{id:guid}")]
        public async Task<ActionResult<ProductDto>> HandleGetProductByIdAsync(Guid id, CancellationToken cancellationToken = new())
        {
            var request = new GetProductById.Query { Id = id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/createProduct")]
        public async Task<ActionResult> HandleCreateProductAsync([FromBody] CreateProduct.Command request, CancellationToken cancellationToken = new())
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    _rabitMQProducer.SendProductMessage(request);

            //}
            return Ok(await Mediator.Send(request, cancellationToken));
        }


        [HttpPost("/api/v{version:apiVersion}/createCategory")]
        public async Task<ActionResult> HandleCreateCategoryAsync([FromBody] CreateCategory.Command request, CancellationToken cancellationToken = new())
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    _rabitMQProducer.SendProductMessage(request);

            //}
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("/api/v{version:apiVersion}/getCategoryById")]
        public async Task<ActionResult> HandleGetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = new())
        {
            var request = new GetCategoryById.Query { Id = id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
