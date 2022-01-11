using Bitnvest.Model;
using Bitnvest.Model.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Bitnvest.Mapeador
{
    public static class PagamentosTarifaMapper
    {
        public static IList<PagamentosDTO> Mapear(IList<PagamentoTarifas> pg)
        {
            var pagamentos = new List<PagamentosDTO>();
            var Check = new Bitmap(Imagens.check, new Size(Imagens.check.Width / 20, Imagens.check.Height / 20));
            var Pendente = new Bitmap(Imagens.circleBlue, new Size(Imagens.circleBlue.Width / 25, Imagens.circleBlue.Height / 25));

          
            foreach (var item in pg)
            {
                pagamentos.Add(new PagamentosDTO { 
                    IdCliente = item.IdCorrentista,
                    NomeTarifa = item.Tarifa.Nome,
                    NomeCliente = item.Correntista.Nome,
                    Status = item.DataPagamento != null ? Check : Pendente,
                    Valor = item.Tarifa.Valor,
                    Vencimento = item.DataVencimento,
                });
            }

            return pagamentos;
        }
    }
}
