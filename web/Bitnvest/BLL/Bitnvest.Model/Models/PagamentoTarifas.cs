using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitnvest.Model.Models
{
    public class PagamentoTarifas
    {
        public int Id { get; set; }
        public int IdCorrentista { get; set; }
        public Correntista Correntista { get; set; }
        public int IdTarifa { get; set; }
        public Tarifa Tarifa { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
