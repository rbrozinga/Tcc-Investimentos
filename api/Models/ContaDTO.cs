namespace api.Models
{
    public class ContaDTO
    {
        public int Numero { get; set; }
        public decimal Saldo { get; set; }
        public string SaldoFormatado { get; set; }
        public decimal Investimento { get; set; }
        public string InvestimentoFormatado { get; set; }
    }
}
