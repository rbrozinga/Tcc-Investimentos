using Bitnvest.DataAcess.Context;
using Bitnvest.DataAcess.Repository;
using Bitnvest.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bitnvest
{
    public partial class Login : Form
    {
        private AdministracaoRepository _admRepo;
        private PagamentoTarifasRepository _pagamentoTarifasRepo;
        private TarifasRepository _tarifaRepo;
        private CorrentistaRepository _correntistaRepo;

        private DbSqlContext _db;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            using (_db = new DbSqlContext())
            {
                _tarifaRepo = new TarifasRepository(_db);
                _pagamentoTarifasRepo = new PagamentoTarifasRepository(_db);
                _correntistaRepo = new CorrentistaRepository(_db);

                var tarifas = _tarifaRepo.SelecionarTodos();
                var pagamentos = _pagamentoTarifasRepo.SelecionarPagamentosDecrescente();
                var correntistas = _correntistaRepo.SelecionarTodos();

                if (pagamentos.Count > 0)
                {
                    foreach (var pagamento in pagamentos)
                    {
                        if (pagamento.DataVencimento == DateTime.Now.Date && pagamento.DataPagamento == null)
                        {
                            pagamento.DataPagamento = DateTime.Now.Date;
                            pagamento.Correntista.Conta.Saldo -= pagamento.Tarifa.Valor;

                            _pagamentoTarifasRepo.Atualizar(pagamento);
                        }
                    }

                    foreach (var correntista in correntistas)
                    {
                        foreach (var tarifa in tarifas)
                        {
                            var meusPagamentos = pagamentos
                                                    .Where(x => x.IdCorrentista == correntista.Id);

                            var numeroPagamentos = meusPagamentos.Count(x =>
                                                    x.DataPagamento == null
                                                    && x.IdTarifa == tarifa.Id);

                            if (numeroPagamentos == 0)
                            {
                                for (int i = 1; i < 4; i++)
                                {
                                    _pagamentoTarifasRepo.Adicionar(new PagamentoTarifas
                                    {
                                        Correntista = correntista,
                                        Tarifa = tarifa,
                                        DataVencimento = DateTime.Now.AddDays(tarifa.PagamentoEmDias * i).Date
                                    });
                                }
                            }
                            else if (numeroPagamentos < 3)
                            {
                                var ultimaDataDessePagamento = pagamentos
                                   .OrderByDescending(x => x.DataVencimento).FirstOrDefault(x => x.IdTarifa == tarifa.Id);

                                var qtd = 3 - numeroPagamentos;

                                for (int i = 0; i < qtd; i++)
                                {
                                    _pagamentoTarifasRepo.Adicionar(new PagamentoTarifas
                                    {
                                        Correntista = correntista,
                                        Tarifa = tarifa,
                                        DataVencimento = ultimaDataDessePagamento.DataVencimento.AddDays(tarifa.PagamentoEmDias).Date
                                    });
                                }

                            }

                        }
                    }
                }
                else
                {
                    foreach (var correntista in correntistas)
                    {
                        foreach (var tarifa in tarifas)
                        {
                            _pagamentoTarifasRepo.Adicionar(new List<PagamentoTarifas>
                            {
                                new PagamentoTarifas
                                {
                                    Correntista = correntista,
                                    Tarifa = tarifa,
                                    DataVencimento = DateTime.Now.AddDays(tarifa.PagamentoEmDias).Date
                                },
                                new PagamentoTarifas
                                {
                                    Correntista = correntista,
                                    Tarifa = tarifa,
                                    DataVencimento = DateTime.Now.AddDays(tarifa.PagamentoEmDias * 2).Date
                                },
                                new PagamentoTarifas
                                {
                                    Correntista = correntista,
                                    Tarifa = tarifa,
                                    DataVencimento = DateTime.Now.AddDays(tarifa.PagamentoEmDias * 3).Date
                                }
                            });
                        }
                    }
                }

                _db.SaveChanges();

            }
        }

        private void Login_Close(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Logar();
        }
        //METODOS INTERNOS
        private void Logar()
        {
            var login = txtLogin.Text;
            var senha = txtSenha.Text;

            if (login.Equals("") || senha.Equals(""))
            {
                MessageBox.Show("Ops! Você precisa digitar o email e a senha", "Erro ao fazer LOGIN");
                return;
            }

            Administracao resultado = null;

            using (_db = new DbSqlContext())
            {
                _admRepo = new AdministracaoRepository(_db);
                resultado = _admRepo.Login(login, senha);
            }

            if (resultado == null)
            {
                MessageBox.Show("Ops! Você digitou um email ou senha inválidos!", "Erro ao fazer LOGIN");
                return;
            }

            this.Hide();
            var formulario = new Formulario(resultado.Email);
            formulario.Show();


        }
    }
}
