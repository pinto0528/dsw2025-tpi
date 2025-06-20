using Microsoft.AspNetCore.Mvc;
using Dsw2025TPI.Data;
using Microsoft.EntityFrameworkCore;
using Dsw2025TPI.Domain.Entities;
using Dsw2025TPI.Domain.Interfaces;
using Dsw2025TPI.Api.Services;
using Dsw2025Ej15.Application.Dtos;

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

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProductAsync([FromBody] ProductModel.Request request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Sku = request.Sku,
                InternalCode = request.InternalCode,
                Name = request.Name,
                Description = request.Description,
                CurrentUnitPrice = request.CurrentUnitPrice,
                StockQuantity = request.StockQuantity
            };

            var createdProduct = await _service.CreateAsync(product);

            return Created();
        }
    }
}
