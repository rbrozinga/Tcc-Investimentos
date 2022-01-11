using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class AdministracaoRepository: BaseRepsitory
    {
        public AdministracaoRepository(DbSqlContext db): base(db)
        {

        }

        public Administracao Adicionar(Administracao adm)
        {
            return _db.Administradores.Add(adm).Entity;
        }

        public Administracao Login(string login, string senha)
        {
            var result = _db.Administradores.FirstOrDefault(x => x.Email == login && x.Senha == senha);
            return result;
        }
    }
}
