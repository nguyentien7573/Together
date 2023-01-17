using Microsoft.AspNetCore.Mvc;
using Together.CustomerService.Core.UseCases.Commands;
using Together.CustomerService.Core.UseCases.Queries;
using Together.Infrastructure.Controller;

namespace Together.CustomerService.Api.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class CustomerController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpPost("/api/v{version:apiVersion}/createCustomer")]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCustomer.Command request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [ApiVersion("1.0")]
        [HttpGet("/api/v{version:apiVersion}/getCustomer/{id:guid}")]
        public async Task<ActionResult> GetAsync([FromBody] GetCustomerById.Query request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
