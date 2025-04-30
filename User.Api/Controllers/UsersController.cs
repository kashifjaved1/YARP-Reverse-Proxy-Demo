using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> _users = new()
        {
            new User(1, "john.doe@example.com", "John Doe"),
            new User(2, "jane.smith@example.com", "Jane Smith")
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(_users);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound() : Ok(user);
        }
    }

    public record User(int Id, string Email, string FullName);
}
