using Microsoft.AspNetCore.Mvc;

namespace Product.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product(1, "Laptop", 999.99m),
            new Product(2, "Phone", 699.99m),
            new Product(3, "Tablet", 399.99m)
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(_products);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product == null ? NotFound() : Ok(product);
        }
    }

    public record Product(int Id, string Name, decimal Price);
}
