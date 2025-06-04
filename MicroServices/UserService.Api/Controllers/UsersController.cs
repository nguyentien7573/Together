using Microsoft.AspNetCore.Mvc;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new()
        {
            new User { Id = 1, Name = "Alice" },
            new User { Id = 2, Name = "Bob" }
        };

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }

    public record User
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
