using Bitnvest.DataAcess.Context;
using Bitnvest.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.DataAcess.Repository
{
    public class MoedaRepository : BaseRepsitory
    {
        public MoedaRepository(DbSqlContext db):base(db)
        {
        }
        public IList<Moeda> SelecionarTodasFiltrados()
        {
            return _db.Moedas.Where(x => x.DataCotacao.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                .OrderByDescending(x => x.Id).Take(3).ToList();
        }
        public IList<Moeda> SelecionarTodas()
        {
            return _db.Moedas.Where(x => x.DataCotacao.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                .ToList();
        }
        public IList<Moeda> SelecionarTodosPorNome(string nome)
        {
            return _db.Moedas.Where(x => x.Nome == nome).ToList();
        }

        public Moeda Adicionar(Moeda moeda)
        {
            moeda.DataCotacao = DateTime.Now;
            return _db.Moedas.Add(moeda).Entity; 
        }
        public Moeda Atualizar(Moeda moeda)
        {
            moeda.DataAtualizacao = DateTime.Now;
            return _db.Moedas.Update(moeda).Entity;
        }
    }
}
