namespace Bitnvest.Model.Models
{
    public class MoedaTransacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Cotacao { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
