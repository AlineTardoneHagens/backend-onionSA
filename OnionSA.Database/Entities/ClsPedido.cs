using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionSA.Database.Entities
{
    public class ClsPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public ClsCliente Cliente { get; set; }
        public int ProdutoId { get; set; }
        public ClsProduto Produto { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
