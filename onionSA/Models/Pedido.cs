namespace OnionSA.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
