using Microsoft.EntityFrameworkCore;
using OnionSA.Data;
using OnionSA.Models;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace OnionSA.Services
{
    public class ImportService
    {
        private readonly OnionContext _context;
        private readonly HttpClient _httpClient;

        public ImportService(OnionContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File not selected");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var documento = worksheet.Cells[row, 1].Text.Replace(".", "").Replace("-", "");
                        var nome = worksheet.Cells[row, 2].Text;
                        var cep = worksheet.Cells[row, 3].Text.Replace("-", "");
                        var produtoNome = worksheet.Cells[row, 4].Text;
                        var numeroPedido = worksheet.Cells[row, 5].Text;
                        var data = DateTime.ParseExact(worksheet.Cells[row, 6].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Documento == documento);
                        if (cliente == null)
                        {
                            cliente = new ClsCliente { Documento = documento, Nome = nome, Cep = cep };
                            _context.Clientes.Add(cliente);
                        }

                        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Nome == produtoNome);
                        if (produto == null)
                        {
                            throw new Exception($"Produto {produtoNome} n�o encontrado no banco de dados.");
                        }

                        var viaCepResponse = await ConsultarViaCep(cep);
                        if (viaCepResponse == null)
                        {
                            throw new Exception($"CEP {cep} n�o encontrado.");
                        }

                        var valorProduto = produto.Valor;
                        var valorFrete = CalcularFrete(viaCepResponse.Uf, valorProduto);
                        var valorFinal = valorProduto + valorFrete;
                        var dataEntrega = CalcularDataEntrega(viaCepResponse.Uf, data);

                        var pedido = new ClsPedido
                        {
                            Cliente = cliente,
                            Produto = produto,
                            NumeroPedido = numeroPedido,
                            Data = data,
                            ValorFinal = valorFinal,
                            DataEntrega = dataEntrega
                        };

                        _context.Pedidos.Add(pedido);
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task<ClsViaCepResponse> ConsultarViaCep(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ClsViaCepResponse>(content);
            }
            return null;
        }

        private decimal CalcularFrete(string uf, decimal valorProduto)
        {
            decimal percentualFrete = uf switch
            {
                "SP" => 0.00m,
                "RJ" or "MG" or "ES" => 0.10m,
                "DF" or "GO" or "MT" or "MS" => 0.20m,
                "PR" or "SC" or "RS" => 0.20m,
                _ => 0.30m
            };

            return valorProduto * percentualFrete;
        }

        private DateTime CalcularDataEntrega(string uf, DateTime dataPedido)
        {
            int diasEntrega = uf switch
            {
                "SP" => 0,
                "RJ" or "MG" or "ES" => 1,
                "DF" or "GO" or "MT" or "MS" => 5,
                "PR" or "SC" or "RS" => 5,
                _ => 10
            };

            if (diasEntrega == 0)
            {
                return dataPedido;
            }

            int diasUteis = 0;
            DateTime dataEntrega = dataPedido;
            while (diasUteis < diasEntrega)
            {
                dataEntrega = dataEntrega.AddDays(1);
                if (dataEntrega.DayOfWeek != DayOfWeek.Saturday && dataEntrega.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasUteis++;
                }
            }

            return dataEntrega;
        }
    }
}
