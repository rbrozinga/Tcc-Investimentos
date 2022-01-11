
using Bitnvest.Model.Enums;
using System;
using System.Collections.Generic;

namespace Bitnvest.Model.Models
{
    public class Transacao
    {
        public Transacao()
        {
            MoedasTransacao = new List<MoedaTransacao>();
        }

        public int Id { get; set; }
        public Conta Conta { get; set; }
        public IList<MoedaTransacao> MoedasTransacao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}
