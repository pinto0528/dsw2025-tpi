using Microsoft.AspNetCore.Mvc;
using Dsw2025TPI.Data;
using Microsoft.EntityFrameworkCore;
using Dsw2025TPI.Domain.Entities;
using Dsw2025TPI.Domain.Interfaces;
using Dsw2025TPI.Api.Services;

namespace Dsw2025TPI.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
        {
            var products = await _service.GetAllAsync();
            return products.Any() ? Ok(products) : NoContent();
        }


    }
}
