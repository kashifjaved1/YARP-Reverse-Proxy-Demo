using Microsoft.AspNetCore.Mvc;

namespace Order.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new()
        {
            new Order(1, DateTime.Now.AddDays(-2), 1, 1499.98m),
            new Order(2, DateTime.Now.AddDays(-1), 2, 2099.96m)
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(_orders);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return order == null ? NotFound() : Ok(order);
        }
    }

    public record Order(int Id, DateTime OrderDate, int CustomerId, decimal TotalAmount);
}
