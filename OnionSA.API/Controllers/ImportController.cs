using Microsoft.AspNetCore.Mvc;
using OnionSA.Services;
using System.Threading.Tasks;

namespace OnionSA.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ImportService _importService;

        public ImportController(ImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("")]
        public async Task<IActionResult> ImportaArquivo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo não selecionado");

            try
            {
                await _importService.ImportaExcel(file);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
