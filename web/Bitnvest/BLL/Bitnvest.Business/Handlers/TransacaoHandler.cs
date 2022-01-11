using Bitnvest.DataAcess.Context;
using Bitnvest.DataAcess.Repository;
using Bitnvest.Model.Models;
using Bitnvest.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Bitnvest.Business.Handlers
{
    public class TransacaoHandler
    {
        private DbSqlContext _db = new DbSqlContext();
        private TransacaoRepository _repo;
        private ContaRepository _conta;
        private MoedaTransacaoRepository _moedaTransacao;

        public TransacaoHandler()
        {
            _repo = new TransacaoRepository(_db);
            _conta = new ContaRepository(_db);
            _moedaTransacao = new MoedaTransacaoRepository(_db);
        }

        public void AdicionarTransacao(Correntista cc, IList<Moeda> moedas, int Moeda1Compra, int Moeda2Compra, int Moeda3Compra)
        {
            var ValorTotal =
               (moedas[2].Cotacao * Moeda1Compra) +
               (moedas[1].Cotacao * Moeda2Compra) +
               (moedas[0].Cotacao * Moeda3Compra);

            if (cc.Conta.Saldo >= ValorTotal)
            {
                var contaCorrente = _conta.Selecionar(cc.Conta.Id);

                contaCorrente.Saldo = contaCorrente.Saldo - ValorTotal;
                cc.Conta.Saldo = cc.Conta.Saldo - ValorTotal;

                var _tr = new Transacao
                {
                    Conta = cc.Conta,
                    TipoTransacao = Model.Enums.TipoTransacaoEnum.Compra,
                    Valor = ValorTotal,
                    MoedasTransacao = new List<MoedaTransacao>
                {
                    new MoedaTransacao
                    {
                        Nome = moedas[0].Nome,
                        Cotacao= moedas[0].Cotacao,
                        Quantidade = Moeda3Compra,
                        ValorTotal = moedas[0].Cotacao * Moeda3Compra

                    },
                    new MoedaTransacao
                    {
                        Nome = moedas[1].Nome,
                        Cotacao= moedas[1].Cotacao,
                        Quantidade = Moeda2Compra,
                        ValorTotal = moedas[1].Cotacao * Moeda2Compra

                    },new MoedaTransacao
                    {
                        Nome = moedas[2].Nome,
                        Cotacao= moedas[2].Cotacao,
                        Quantidade = Moeda1Compra,
                        ValorTotal = moedas[2].Cotacao * Moeda1Compra

                    }
                }
                };

                _repo.Atualizar(_tr);
                _conta.Atualizar(contaCorrente);

                _db.SaveChanges();
            }
        }

        public void SacardaConta(decimal ValorSaqueFloat, Conta cc)
        {

            var conta = _conta.Selecionar(cc.Id);
            conta.Saldo -= ValorSaqueFloat;
            _conta.Atualizar(conta);
        }

        public void AdicionarTransacaoVenda(Correntista cc, IList<Moeda> moedas, int Moeda1Compra, int Moeda2Compra, int Moeda3Compra)
        {
            var ValorTotal =
               (moedas[2].Cotacao * Moeda1Compra) +
               (moedas[1].Cotacao * Moeda2Compra) +
               (moedas[0].Cotacao * Moeda3Compra);

            var contaCorrente = _conta.Selecionar(cc.Conta.Id);

            contaCorrente.Saldo = contaCorrente.Saldo + ValorTotal;
            cc.Conta.Saldo = cc.Conta.Saldo + ValorTotal;

            var _tr = new Transacao
            {
                Conta = cc.Conta,
                TipoTransacao = Model.Enums.TipoTransacaoEnum.Venda,
                Valor = ValorTotal,
                MoedasTransacao = new List<MoedaTransacao>
                {
                    new MoedaTransacao
                    {
                        Nome = moedas[0].Nome,
                        Cotacao= moedas[0].Cotacao,
                        Quantidade = Moeda3Compra,
                        ValorTotal = moedas[0].Cotacao * Moeda3Compra

                    },
                    new MoedaTransacao
                    {
                        Nome = moedas[1].Nome,
                        Cotacao= moedas[1].Cotacao,
                        Quantidade = Moeda2Compra,
                        ValorTotal = moedas[1].Cotacao * Moeda2Compra

                    },new MoedaTransacao
                    {
                        Nome = moedas[2].Nome,
                        Cotacao= moedas[2].Cotacao,
                        Quantidade = Moeda1Compra,
                        ValorTotal = moedas[2].Cotacao * Moeda1Compra

                    }
                }
            };

            _repo.Atualizar(_tr);
            _conta.Atualizar(contaCorrente);

            _db.SaveChanges();

        }

        public ListaMoedasViewDTO SelecionarTransacoesUsuario(Correntista cc)
        {
            var transacoes = _repo.SelecionarPorConta(cc.Conta.Id);

            var compra = transacoes.Where(x => x.TipoTransacao == Model.Enums.TipoTransacaoEnum.Compra).ToList();
            var venda = transacoes.Where(x => x.TipoTransacao == Model.Enums.TipoTransacaoEnum.Venda).ToList();

            var ListaMoedasCompra = RetornaListaMoedasClassificadasPorNome(compra);
            var ListaMoedasVenda = RetornaListaMoedasClassificadasPorNome(venda);

            var moeda1ListaCompra = ConverteListaMoedas(ListaMoedasCompra[0]);
            var moeda2ListaCompra = ConverteListaMoedas(ListaMoedasCompra[1]);
            var moeda3ListaCompra = ConverteListaMoedas(ListaMoedasCompra[2]);

            var moeda1ListaVenda = ConverteListaMoedas(ListaMoedasVenda[0]);
            var moeda2ListaVenda = ConverteListaMoedas(ListaMoedasVenda[1]);
            var moeda3ListaVenda = ConverteListaMoedas(ListaMoedasVenda[2]);


            var valorMoeda1 = moeda1ListaCompra.Valor - moeda1ListaVenda.Valor;
            var valorMoeda2 = moeda2ListaCompra.Valor - moeda2ListaVenda.Valor;
            var valorMoeda3 = moeda3ListaCompra.Valor - moeda3ListaVenda.Valor;

            var quantidadeMoedas1 = moeda1ListaCompra.Quantidade - moeda1ListaVenda.Quantidade;
            var quantidadeMoedas2 = moeda2ListaCompra.Quantidade - moeda2ListaVenda.Quantidade;
            var quantidadeMoedas3 = moeda3ListaCompra.Quantidade - moeda3ListaVenda.Quantidade;

            ZerarMoedas(valorMoeda1, quantidadeMoedas1, "Moeda 1", compra);
            ZerarMoedas(valorMoeda1, quantidadeMoedas1, "Moeda 1", venda);

            ZerarMoedas(valorMoeda2, quantidadeMoedas2, "Moeda 2", compra);
            ZerarMoedas(valorMoeda2, quantidadeMoedas2, "Moeda 2", venda);

            ZerarMoedas(valorMoeda3, quantidadeMoedas3, "Moeda 3", compra);
            ZerarMoedas(valorMoeda3, quantidadeMoedas3, "Moeda 3", venda);

            var valorInvestido = valorMoeda1 + valorMoeda2 + valorMoeda3;

            _db.SaveChanges();

            List<ListaMoedasDTO> _resultado = new List<ListaMoedasDTO>
            {
                new ListaMoedasDTO
                {
                    Nome = moeda1ListaCompra.Nome,
                    Quantidade = quantidadeMoedas1,
                    Valor = valorMoeda1 < 0 ? 0.0M : quantidadeMoedas1 == 0 ? 0.0M : valorMoeda1,
                },
                 new ListaMoedasDTO
                {
                    Nome = moeda2ListaCompra.Nome,
                    Quantidade = quantidadeMoedas2,
                    Valor = valorMoeda2 < 0 ? 0.0M : quantidadeMoedas2 == 0 ? 0.0M : valorMoeda2,
                },
                  new ListaMoedasDTO
                {
                    Nome = moeda3ListaCompra.Nome,
                    Quantidade = quantidadeMoedas3,
                    Valor = valorMoeda3 < 0 ? 0.0M : quantidadeMoedas3 == 0 ? 0.0M : valorMoeda3,
                },

            };


            return new ListaMoedasViewDTO
            {
                ValorTotal = valorInvestido,
                ListaMoedas = _resultado
            };


        }

        private void ZerarMoedas(decimal valorMoeda, int quantidadeMoedas, string nomeMoeda, List<Transacao> transacoes)
        {
            if (valorMoeda < 0.0M || quantidadeMoedas <= 0)
            {
                foreach (var item in transacoes)
                {
                    var m = item.MoedasTransacao.First(x => x.Nome == nomeMoeda);
                    m.Quantidade = 0;
                    m.ValorTotal = 0.0M;
                }

                _repo.AtualizarMuitos(transacoes);
            }
        }

        private List<MoedaTransacao>[] RetornaListaMoedasClassificadasPorNome(List<Transacao> trasacoes)
        {

            var moedas1 = new List<MoedaTransacao>();
            var moedas2 = new List<MoedaTransacao>();
            var moedas3 = new List<MoedaTransacao>();

            foreach (var item in trasacoes)
            {
                moedas1.Add(item.MoedasTransacao.First(x => x.Nome == "Moeda 1"));
                moedas2.Add(item.MoedasTransacao.First(x => x.Nome == "Moeda 2"));
                moedas3.Add(item.MoedasTransacao.First(x => x.Nome == "Moeda 3"));
            }

            return new List<MoedaTransacao>[3]
            {
                moedas1,
                moedas2,
                moedas3
            };
        }

        private ListaMoedasDTO ConverteListaMoedas(List<MoedaTransacao> moeda)
        {
            if (moeda.Count != 0)
            {
                return moeda
               .Select(x => new ListaMoedasDTO
               {
                   Nome = x.Nome,
                   Quantidade = moeda.Select(y => y.Quantidade).Sum(),
                   Valor = moeda.Select(y => y.ValorTotal).Sum()
               }).First();
            }

            return new ListaMoedasDTO();
        }
    }
}
