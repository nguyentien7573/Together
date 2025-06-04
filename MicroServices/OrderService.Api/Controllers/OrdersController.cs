using Microsoft.AspNetCore.Mvc;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly List<Order> Orders = new();

        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var client = _httpClientFactory.CreateClient("UserService");
            var user = await client.GetFromJsonAsync<User>($"users/{order.UserId}");
            if (user == null)
            {
                return BadRequest($"User {order.UserId} not found");
            }
            order.Id = Orders.Count + 1;
            Orders.Add(order);
            return Ok(order);
        }
    }

    public record Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Item { get; set; } = string.Empty;
    }

    public record User(int Id, string Name);
}
