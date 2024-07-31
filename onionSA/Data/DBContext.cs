using Microsoft.EntityFrameworkCore;
using OnionSA.Models;

namespace OnionSA.Data
{
    public class OnionContext : DbContext
    {
        public OnionContext(DbContextOptions<OnionContext> options) : base(options) { }

        public DbSet<ClsProduto> Produtos { get; set; }
        public DbSet<ClsCliente> Clientes { get; set; }
        public DbSet<ClsPedido> Pedidos { get; set; }

        //Add as seeds de produtos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClsProduto>().HasData(
                new ClsProduto { Id = 1, Nome = "Celular", Valor = 1000 },
                new ClsProduto { Id = 2, Nome = "Notebook", Valor = 3000 },
                new ClsProduto { Id = 3, Nome = "Televisão", Valor = 5000 }
            );
        }
    }
}
