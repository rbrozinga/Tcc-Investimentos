using System;
using System.Collections.Generic;

namespace Bitnvest.Model.Models
{
    public class Correntista
    {
        public Correntista()
        {
            Tarifas = new List<PagamentoTarifas>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cel { get; set; }
        public int IdConta { get; set; }
        public Conta Conta { get; set; }
        public virtual IList<PagamentoTarifas> Tarifas { get; set; }

    }
}
