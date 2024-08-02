using OnionSA.Database.Entities;
using OnionSA.Database.Data;
using System.Linq;
using System.Threading.Tasks;
using OnionSA.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnionSA.Services
{
    public class PedidosService : IPedidosService
    {
        private readonly OnionContext _context;

        public PedidosService(OnionContext context)
        {
            _context = context;
        }

        public async Task<object> CalculaVendasPorRegiao()
        {
             var salesByRegion = await _context.Pedidos
                .Include(p => p.Cliente)
                .ToListAsync();

            var result = salesByRegion.GroupBy(p => BuscaRegiaoPorCep(p.Cliente.Cep))
                .Select(g => new
                {
                    Region = g.Key,
                    TotalSales = g.Sum(p => p.ValorFinal)
                });

            return result;
        }

        public async Task<object> CalculaVendasPorProduto()
        {
            var salesByProduct = await _context.Pedidos
                .Include(p => p.Produto)
                .ToListAsync();

            var result = salesByProduct.GroupBy(p => p.Produto.Nome)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalSales = g.Sum(p => p.ValorFinal)
                });

            return result;
        }

        public async Task<object> ListaPedidos()
        {
            var orders = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Produto)
                .Select(p => new
                {
                    Cliente = p.Cliente.Nome,
                    Produto = p.Produto.Nome,
                    ValorFinal = p.ValorFinal,
                    DataEntrega = p.DataEntrega
                })
                .ToListAsync();

            return orders;
        }

        private string BuscaRegiaoPorCep(string cep)
        {
            var regiao = int.Parse(cep.Substring(0, 1));
            return regiao switch
            {
                0 or 1 or 2 or 3 => "Sudeste",
                4 or 5 or 6 => "Centro-oeste/Sul",
                7 or 8 or 9 => "Norte/Nordeste",
                _ => "Desconhecida"
            };
        }
    }
}
