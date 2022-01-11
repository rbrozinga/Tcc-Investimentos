using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bitnvest.Model
{
    public class PagamentosDTO
    {
        [DisplayName("Identificador Cliente")]
        public int IdCliente { get; set; }

        [DisplayName("Nome da Receita")]
        public string NomeTarifa { get; set; }

        [DisplayName("Cliente")]
        public string NomeCliente { get; set; }

        [DisplayName("Status")]
        public Image Status { get; set; }

        [DisplayName("Vencimento")]
        public DateTime Vencimento { get; set; }

        [DisplayName("Valor")]
        public decimal Valor { get; set; }
    }
}
