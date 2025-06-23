using Dsw2025TPI.Application.DTOs;
using Dsw2025TPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025TPI.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OrderRequest request)
        {
            var result = await _orderService.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return Ok();
        }
    }
}
