using Bitnvest.Business.Handlers;
using Bitnvest.Model.Models;
using Bitnvest.Model.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitnvest.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        CorrentistaHandler _handler = new CorrentistaHandler();
        MoedaHandler _moedaHandler = new MoedaHandler();
        TransacaoHandler _transacaoHandler = new TransacaoHandler();

        Transacao _tr;
        MoedaTransacao _moedaTrans;

        [BindProperty]
        public Correntista Correntista { get; set; }

        [BindProperty]
        public IList<Moeda> Moedas { get; set; }

        [BindProperty]
        public decimal[] ValorMoeda { get; set; }

        [BindProperty]
        public string[] DatasMoeda { get; set; }

        [BindProperty]
        public Conta Conta { get; set; }

        [BindProperty]
        public PercentualDTO Percentual { get; set; }

        [BindProperty]
        public ListaMoedasViewDTO ListaMoedas { get; set; }

        public DashboardModel()
        {
            Correntista = new Correntista();
            Moedas = new List<Moeda>();
            Conta = new Conta();
            _tr = new Transacao();
            _moedaTrans = new MoedaTransacao();
            ListaMoedas = new ListaMoedasViewDTO();
        }

        public void OnGet()
        {
            _moedaHandler.AdicionarCotacoes();
            RecarregarDados();
        }

        public IActionResult OnPostAdicionarSaldo()
        {
            Correntista = _handler.SelecionarCorrentista(User.Identity.Name);
            _handler.AdicionarSaldo(Correntista);
            RecarregarDados();
            return RedirectToPage("Dashboard");
        }

        public IActionResult OnPostInvestir(int Moeda1Compra, int Moeda2Compra, int Moeda3Compra)
        {
            RecarregarDados();
            _transacaoHandler.AdicionarTransacao(Correntista, Moedas, Moeda1Compra, Moeda2Compra, Moeda3Compra);
            RecarregarDados(); 
            return RedirectToPage("Dashboard");
        }

        public IActionResult OnPostVender(int Moeda1Compra, int Moeda2Compra, int Moeda3Compra)
        {
            RecarregarDados();
            _transacaoHandler.AdicionarTransacaoVenda(Correntista, Moedas, Moeda1Compra, Moeda2Compra, Moeda3Compra);
            RecarregarDados();
            return RedirectToPage("Dashboard");
        }

        public IActionResult OnPostSacar(decimal ValorSaque)
        {
            RecarregarDados();
            _transacaoHandler.SacardaConta(ValorSaque, Correntista.Conta);
            RecarregarDados();
            return RedirectToPage("Dashboard");
        }

        private void RecarregarDados()
        {
            if (User.Identity.IsAuthenticated)
            {

                Moedas = _moedaHandler.Selecionar();
                Correntista = _handler.SelecionarCorrentista(User.Identity.Name);

                ListaMoedas = _transacaoHandler.SelecionarTransacoesUsuario(Correntista);

                var moeda = _moedaHandler.PegandoMoedas();

                ValorMoeda = new decimal[moeda.Count];
                DatasMoeda = new string[moeda.Count];

                _moedaHandler.AdicionarCotacoes();

                for (int i = 0; i < moeda.Count; i++)
                {
                    ValorMoeda.SetValue(moeda[i].Cotacao, i);
                    DatasMoeda.SetValue(moeda[i].DataAtualizacao.Value.ToString("HH") + "h", i);
                }

                if (ValorMoeda.Count() >= 2)
                {
                    var valorUm = ValorMoeda[ValorMoeda.Count() - 2];
                    var valorDois = ValorMoeda[ValorMoeda.Count() - 1];
                    Percentual = new PercentualDTO();

                    if (valorUm > valorDois)
                    {
                        Percentual.Valor = (valorUm - valorDois);
                        Percentual.Sinal = false;
                    }
                    else
                    {
                        Percentual.Valor = (valorDois - valorUm);
                        Percentual.Sinal = true;
                    }
                } else
                {
                    Percentual = new PercentualDTO();
                    Percentual.Valor = 0.0M;
                    Percentual.Sinal = true;
                }

            }
        }
    }
}
