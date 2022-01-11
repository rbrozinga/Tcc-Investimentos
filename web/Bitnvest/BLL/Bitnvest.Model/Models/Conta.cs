using System.Collections.Generic;

namespace Bitnvest.Model.Models
{
    public class Conta
    {
        public Conta()
        {
            Transacoes = new List<Transacao>();
        }

        public int Id { get; set; }
        public int Numero { get; set; }
        public decimal Saldo { get; set; }
        public IList<Transacao> Transacoes { get; set; }
        public Correntista Correntista { get; set; }
    }
}
