using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class TarifasRepository: BaseRepsitory
    {
        public TarifasRepository(DbSqlContext db): base(db)
        {

        }

        public IList<Tarifa> SelecionarTodos()
        {
            return _db.Tarifas.ToList();
        }

        public Tarifa Adicioanar(Tarifa tf)
        {
            return _db.Tarifas.Add(tf).Entity;
        }
    }
}
