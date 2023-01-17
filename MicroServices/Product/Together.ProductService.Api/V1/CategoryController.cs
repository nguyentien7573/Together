using Microsoft.AspNetCore.Mvc;
using Together.Infrastructure.Controller;
using Together.ProductService.Core.UseCases.Commands.Categories;
using Together.ProductService.Core.UseCases.Queries.Categories;

namespace Together.ProductService.Api.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : BaseController
    {
        [HttpGet("/api/v{version:apiVersion}/getCategoryById")]
        public async Task<ActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = new())
        {
            var request = new GetCategoryById.Query { Id = id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/createCategory")]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCategory.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPut("/api/v{version:apiVersion}/updateCategory")]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCategory.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }


        [HttpDelete("/api/v{version:apiVersion}/deleteCategory")]
        public async Task<ActionResult> DeleteAsync(Guid Id, CancellationToken cancellationToken = new())
        {
            var request = new DeleteCategory.Command { Id = Id };
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
