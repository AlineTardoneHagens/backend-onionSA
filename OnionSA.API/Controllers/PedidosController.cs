using Microsoft.AspNetCore.Mvc;
using OnionSA.Services;

namespace OnionSA.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosService _salesService;

        public PedidosController(PedidosService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("regiao")]
        public async Task<IActionResult> CalculaVendasPorRegiao()
        {
            var result = await _salesService.CalculaVendasPorRegiao();
            return Ok(result);
        }

        [HttpGet("produto")]
        public async Task<IActionResult> CalculaVendasPorProduto()
        {
            var result = await _salesService.CalculaVendasPorProduto();
            return Ok(result);
        }

        [HttpGet("lista")]
        public async Task<IActionResult> ListaPedidos()
        {
            var result = await _salesService.ListaPedidos();
            return Ok(result);
        }
    }
}
