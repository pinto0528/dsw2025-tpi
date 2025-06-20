using Microsoft.AspNetCore.Mvc;
using Dsw2025TPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025TPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly Dsw2025TpiContext _context;

        public ProductsController(Dsw2025TpiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}
