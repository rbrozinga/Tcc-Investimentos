using System;

namespace Bitnvest.Model.Models
{
    public class Moeda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Cotacao { get; set; }
        public DateTime DataCotacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
