using Microsoft.EntityFrameworkCore;
using OnionSA.Database.Entities;

namespace OnionSA.Database.Data
{
    public class OnionContext : DbContext
    {
        public OnionContext(DbContextOptions<OnionContext> options) : base(options) { }

        public DbSet<ClsProduto> Produtos { get; set; }
        public DbSet<ClsCliente> Clientes { get; set; }
        public DbSet<ClsPedido> Pedidos { get; set; }
    }
}
