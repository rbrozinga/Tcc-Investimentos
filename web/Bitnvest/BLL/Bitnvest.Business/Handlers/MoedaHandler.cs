using Bitnvest.DataAcess.Context;
using Bitnvest.DataAcess.Repository;
using Bitnvest.Model.Models;
using System;
using System.Collections.Generic;

namespace Bitnvest.Business.Handlers
{
    public class MoedaHandler
    {
        private DbSqlContext _db = new DbSqlContext();
        private MoedaRepository _moedaRepo;

        public MoedaHandler()
        {
            _moedaRepo = new MoedaRepository(_db);
        }

        public IList<Moeda> Selecionar()
        {
            var Moedas = _moedaRepo.SelecionarTodasFiltrados();
            return Moedas;
        }

        public void AdicionarCotacoes()
        {
            var Moedas = _moedaRepo.SelecionarTodasFiltrados();

            if (Moedas.Count != 0)
            {
                var horaAtualizacao = Moedas[0].DataAtualizacao.Value.ToString("HH");
                var horaHoje = DateTime.Now.ToString("HH");

                if (horaAtualizacao != horaHoje)
                {
                    var cotacoes = GerandoAsCotacoes();
                    foreach (var moeda in cotacoes)
                    {
                        _moedaRepo.Adicionar(moeda);
                    }
                }

                //foreach (var money in testeMoedas)
                //{
                //    var moneyDataAtualizacao = money.DataAtualizacao.Value.ToString("dd/MM/yyyy");
                //    var dataHoje = DateTime.Now.ToString("dd/MM/yyyy");

                //    if (moneyDataAtualizacao != dataHoje)
                //    {
                //        money.Cotacao = Math.Round(money.Cotacao * (decimal)random.NextDouble(), 2);
                //        money.DataAtualizacao = DateTime.Now;
                //        _moedaRepo.Atualizar(money);
                //    }
                //}
            }
            else
            {

                var cotacoes = GerandoAsCotacoes();

                foreach (var moeda in cotacoes)
                {
                    _moedaRepo.Adicionar(moeda);
                }
            }

            _db.SaveChanges();
        }

        public IList<Moeda> PegandoMoedas()
        {
            var Moedas = _moedaRepo.SelecionarTodosPorNome("Moeda 1");
            return Moedas;
        }

        private IList<Moeda> GerandoAsCotacoes()
        {
            var random = new Random();
            List<Moeda> moedas = new List<Moeda>
                {
                    new Moeda {
                    Nome = "Moeda 1",
                    Cotacao = Math.Round(10.0M * (decimal) random.NextDouble() + 1M, 2),
                    DataCotacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Quantidade = 10000
                    },
                    new Moeda {
                    Nome = "Moeda 2",
                    Cotacao = Math.Round(8.0M * (decimal) random.NextDouble() + 1M, 2),
                    DataCotacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Quantidade = 10000
                    },
                    new Moeda {
                    Nome = "Moeda 3",
                    Cotacao = Math.Round(13.0M * (decimal) random.NextDouble() + 1M, 2),
                    DataCotacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Quantidade = 10000
                    }
                };
            return moedas;
        }
    }
}
