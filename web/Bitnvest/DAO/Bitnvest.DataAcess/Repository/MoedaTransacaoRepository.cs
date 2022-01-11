using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class MoedaTransacaoRepository : BaseRepsitory
    {
        public MoedaTransacaoRepository(DbSqlContext db): base(db)
        {

        }

        public List<MoedaTransacao> SelecionarPorNomeTransacaoId(string nomeMoeda, int transacaoId)
        {
            //return _db.MoedaTransacoes.Where(x => x.Tra)
            return null;
        }
    }
}
