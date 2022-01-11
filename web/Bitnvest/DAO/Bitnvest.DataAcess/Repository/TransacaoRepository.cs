using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class TransacaoRepository: BaseRepsitory
    {
        public TransacaoRepository(DbSqlContext db): base(db)
        {
        }

        public List<Transacao> SelecionarPorConta(int id)
        {
            return _db.Transacoes
                .Include(x => x.MoedasTransacao)
                .Include(x => x.Conta)
                .Where(x => x.Conta.Id == id).ToList();
        }
        public List<Transacao> SelecionarPorCorrentista(int id)
        {
            return _db.Transacoes
                .Include(x => x.MoedasTransacao)
                .Include(x => x.Conta)
                .ThenInclude(x => x.Correntista)
                .Where(x => x.Conta.Correntista.Id == id).ToList();
        }
        public Transacao SelecionarPorId(int id)
        {
            return _db.Transacoes.FirstOrDefault(x => x.Id == id);
        }
        public Transacao Adicionar(Transacao trans)
        {
            trans.DataTransacao = DateTime.Now;
            return _db.Transacoes.Add(trans).Entity;
        }
        public Transacao Atualizar(Transacao trans)
        {
            trans.DataTransacao = DateTime.Now;
            return _db.Transacoes.Update(trans).Entity;
        }

        public void AtualizarMuitos(List<Transacao> trans)
        {
            _db.Transacoes.UpdateRange(trans);
        }
    }
}
