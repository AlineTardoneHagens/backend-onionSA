using Microsoft.AspNetCore.Http;

namespace OnionSA.Domain.Services.Interfaces
{
    public interface IImportService
    {
        Task ImportExcel(IFormFile file);
    }
}
