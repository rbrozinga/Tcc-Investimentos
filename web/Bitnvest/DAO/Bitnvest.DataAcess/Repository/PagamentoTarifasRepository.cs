using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class PagamentoTarifasRepository : BaseRepsitory
    {
        public PagamentoTarifasRepository(DbSqlContext db): base(db)
        {

        }

        public IList<PagamentoTarifas> SelecionarTodosPagamentos()
        {
            return _db.PagamentosTarifas.Include(x => x.Correntista).Include(x => x.Tarifa).ToList();
        }
        public IList<PagamentoTarifas> SelecionarPagamentosDistintos()
        {
            return _db.PagamentosTarifas.Include(x => x.Correntista).Include(x => x.Tarifa).Distinct().ToList();
        }

        public IList<PagamentoTarifas> SelecionarPagamentosDecrescente()
        {
            return _db.PagamentosTarifas.Include(x => x.Correntista).ThenInclude(a => a.Conta)
                .Include(x => x.Tarifa).OrderByDescending(x => x.DataVencimento).ToList();
        }

        public PagamentoTarifas Adicionar(PagamentoTarifas pt)
        {
            return _db.PagamentosTarifas.Add(pt).Entity;
        }

        public void Adicionar(IList<PagamentoTarifas> pt)
        {
            _db.PagamentosTarifas.AddRange(pt);
        }

        public PagamentoTarifas Atualizar(PagamentoTarifas pt)
        {
            return _db.PagamentosTarifas.Update(pt).Entity;
        }
    }
}
