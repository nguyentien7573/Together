using Microsoft.AspNetCore.Mvc;
using Together.Infrastructure.Controller;
using Together.ProductService.Core.UseCases.Commands;
using Together.ProductService.Core.UseCases.Queries;

namespace Together.ProductService.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : BaseController
    {
        [HttpGet("/api/v{version:apiVersion}/getCategoryById")]
        public async Task<ActionResult> HandleGetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = new())
        {
            var request = new GetCategoryById.Query { Id = id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/createCategory")]
        public async Task<ActionResult> HandleCreateCategoryAsync([FromBody] CreateCategory.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
