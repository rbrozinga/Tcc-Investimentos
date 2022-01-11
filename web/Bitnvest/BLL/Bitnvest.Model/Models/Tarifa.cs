using System;
using System.Collections.Generic;
using System.Text;

namespace Bitnvest.Model.Models
{
   public class Tarifa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int PagamentoEmDias { get; set; }
        public virtual IList<PagamentoTarifas> Pagamentos { get; set; }
    }
}
