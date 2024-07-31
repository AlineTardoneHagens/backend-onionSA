using Microsoft.AspNetCore.Mvc;
using OnionSA.Services;

namespace OnionSA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly PedidosService _salesService;

        public SalesController(PedidosService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("region")]
        public async Task<IActionResult> GetSalesByRegion()
        {
            var result = await _salesService.GetSalesByRegion();
            return Ok(result);
        }

        [HttpGet("product")]
        public async Task<IActionResult> GetSalesByProduct()
        {
            var result = await _salesService.GetSalesByProduct();
            return Ok(result);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _salesService.GetOrders();
            return Ok(result);
        }
    }
}
