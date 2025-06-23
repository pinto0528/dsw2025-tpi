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

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductModel.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductModel.Response>> UpdateProductAsync(Guid id, [FromBody] ProductModel.Request request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, request);
        return Ok(updated);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateProductAsync(Guid id)
    {
        await _service.DeactivateAsync(id);
        return NoContent(); // 204: éxito sin cuerpo
    }

}
