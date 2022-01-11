using System;
using System.Collections.Generic;
using System.Text;

namespace Bitnvest.Model.ModelView
{
    public class ListaMoedasViewDTO
    {
        public decimal ValorTotal { get; set; }
        public List<ListaMoedasDTO> ListaMoedas { get; set; }
    }
}
