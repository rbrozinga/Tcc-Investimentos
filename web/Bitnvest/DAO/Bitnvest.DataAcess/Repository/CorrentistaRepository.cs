using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class CorrentistaRepository : BaseRepsitory
    {
        public CorrentistaRepository(DbSqlContext db):base(db)
        {
        }

        public Correntista SelecionarPeloId(int id)
        {
            return _db.Correntistas
                .Include(x => x.Conta)
                .Include(x => x.Conta.Transacoes)
                .FirstOrDefault(x => x.Id == id);
        }
        public Correntista SelecionarPeloCPF(string cpf)
        {
            return _db.Correntistas.FirstOrDefault(x => x.CPF == cpf);
        }
        public Correntista SelecionarPeloEmail(string email)
        {
            return _db.Correntistas.Include(x => x.Conta).FirstOrDefault(x => x.Email == email);
        }
        public IList<Correntista> SelecionarTodos(int skip, int take, int numeroOcorrorencias)
        {
            return _db.Correntistas.Skip(skip).Take(take).ToList();
        }
        public IList<Correntista> SelecionarTodos()
        {
            return _db.Correntistas.Include(x => x.Conta).ToList();
        }
        public Correntista Inserir(Correntista cc)
        {
            return _db.Correntistas.Add(cc).Entity;
        }
        public Correntista Atualizar(Correntista cc)
        {
            return _db.Correntistas.Add(cc).Entity;
        }
        public Correntista SelecionarPeloLogin(string email, string senha)
        {
            return _db.Correntistas.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }
    }
}
