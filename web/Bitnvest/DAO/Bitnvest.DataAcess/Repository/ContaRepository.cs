using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class ContaRepository
    {
        private DbSqlContext _db;
        public ContaRepository(DbSqlContext db)
        {
            _db = db;
        }

        public Conta Selecionar(int id)
        {
            return _db.Contas.FirstOrDefault(x => x.Id == id);
        }
        public Conta Inserir(Conta cc)
        {
            var conta = _db.Contas.Add(cc);
            return conta.Entity;
        }
        public Conta Atualizar(Conta cc)
        {
            return _db.Update(cc).Entity;
        }

        
    }
}
