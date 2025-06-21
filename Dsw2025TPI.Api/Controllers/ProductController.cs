using Dsw2025Ej15.Application.Dtos;
using Dsw2025TPI.Api.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<ProductModel.Response>>> GetAllProductsAsync()
    {
        var products = await _service.GetAllAsync();
        return products.Any() ? Ok(products) : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductModel.Response>> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductModel.Response>> CreateProductAsync([FromBody] ProductModel.Request request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdProduct = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }
}
