namespace OnionSA.Models
{
    public class ClsCliente
    {
        public int Id { get; set; }
        public string Documento { get; set; }  // CPF ou CNPJ sem pontos ou traços
        public string Nome { get; set; }
        public string Cep { get; set; }
    }
}
