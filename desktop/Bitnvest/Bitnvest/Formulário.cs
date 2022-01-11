using Bitnvest.DataAcess.Context;
using Bitnvest.DataAcess.Repository;
using Bitnvest.Mapeador;
using Bitnvest.Model.Models;
using System;
using System.Windows.Forms;

namespace Bitnvest
{
    public partial class Formulario : Form
    {
        public readonly string EMAIL;
        DbSqlContext db;

        AdministracaoRepository _admRepo;
        TarifasRepository _tarifasRepo;
        TransacaoRepository _transacaoRepo;
        PagamentoTarifasRepository _pagamentoTarifasRepo;


        public Formulario(string email)
        {
            InitializeComponent();
            EMAIL = email;
            //db = new DbSqlContext();
            //_admRepo = new AdministracaoRepository(db);
            //_tarifasRepo = new TarifasRepository(db);
            //_pagamentoTarifasRepo = new PagamentoTarifasRepository(db);
        }

        private void Formulario_Load(object sender, EventArgs e)
        {
            lblEmail.Text = EMAIL;

            CarregandoDados();

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Ação";
            btn.Name = "button";
            btn.Text = "Exportar Relatório";
            btn.UseColumnTextForButtonValue = true;
            btn.Width = 130;

            dataGridViewRelatorios.Columns.Add(btn);
        }
        private void Formulario_Close(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            var login = new Login();
            this.Hide();
            login.Show();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            OcultarPaineis();
            panelHome.Visible = true;
        }
        private void btnRelatorio_Click(object sender, EventArgs e)
        {

            //CarregandoDados();
            OcultarPaineis();
            panelRelatorios.Visible = true;

        }
        private void dataGridViewRelatorios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                var clienteId = (int) dataGridViewRelatorios.Rows[e.RowIndex].Cells[0].Value;

                using (db = new DbSqlContext())
                {
                    _transacaoRepo = new TransacaoRepository(db);
                    var transacoes = _transacaoRepo.SelecionarPorCorrentista(clienteId);

                    if (transacoes.Count <= 0)
                    {
                        MessageBox.Show("Não existem transações para esse cliente!");
                        return;
                    }


                    saveExcel.FileName = $"transacao_{transacoes[0].Conta.Numero}_{DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")}";

                    var resultado = saveExcel.ShowDialog();

                    if(resultado == DialogResult.OK)
                    {
                        Export.Export.ExportToExcel(transacoes, saveExcel.FileName);
                    }


                }

            }
        }

        private void btnRh_Click(object sender, EventArgs e)
        {
            OcultarPaineis();
            panelRecursosHumanos.Visible = true;
        }
        private void btnFinanceiro_Click(object sender, EventArgs e)
        {
            OcultarPaineis();
            panelTarifas.Visible = true;

        }
        private void OcultarPaineis()
        {
            panelHome.Visible = false;
            panelRecursosHumanos.Visible = false;
            panelRelatorios.Visible = false;
            panelTarifas.Visible = false;

        }
        private void btnAdicionarColaborador_Click(object sender, EventArgs e)
        {
            try
            {
                var colaborador = new Model.Models.Administracao
                {
                    Email = txtEmail.Text.ToLower().Trim(),
                    Nome = txtNome.Text.ToUpper().Trim(),
                    Senha = txtSenha.Text.Trim()
                };


                if (colaborador.Email.Equals("") || colaborador.Nome.Equals("") || colaborador.Senha.Equals(""))
                {
                    MessageBox.Show("Todos os campos são obrigatórios!", "Colaborador");
                    return;
                }

                using (db = new DbSqlContext())
                {
                    _admRepo = new AdministracaoRepository(db);
                    var result = _admRepo.Adicionar(colaborador);
                    db.SaveChanges();
                }

                MessageBox.Show("Colborador adicionado com Sucesso!", "Colaborador");
            }
            catch (Exception)
            {
                db.Dispose();
                MessageBox.Show("Erro ao tentar adicionar Colaborador", "Colaborador");
            }
        }
        private void lblEmail_Click(object sender, EventArgs e)
        {

        }
        private void btnAddTarifa_Click(object sender, EventArgs e)
        {

            try
            {
                var error = false;
                var diasCob = txtDiasCobranca.Text.Trim();

                var tarifa = new Tarifa
                {
                    Nome = txtNomeTarifa.Text,
                    Valor = decimal.Parse(String.IsNullOrEmpty(txtValorTarifa.Text.Replace(",", "").Trim()) ? "0" : txtValorTarifa.Text),
                    PagamentoEmDias = int.Parse(String.IsNullOrEmpty(diasCob) ? "0" : diasCob)
                };

                if (tarifa.Nome.Equals(""))
                {
                    MessageBox.Show("O Nome da Tarifa é obrigatório!", "Tarifas");
                    error = true;
                }

                if (tarifa.Valor <= 0M)
                {
                    MessageBox.Show("O Valor está incorreto!", "Tarifas");
                    error = true;
                }

                if (tarifa.PagamentoEmDias <= 0)
                {
                    MessageBox.Show("O Numero de dias para Cobrança está incorreto!", "Tarifas");
                    error = true;
                }

                if (error)
                {
                    return;
                }

                using (db = new DbSqlContext())
                {
                    _tarifasRepo = new TarifasRepository(db);
                    _tarifasRepo.Adicioanar(tarifa);
                    db.SaveChanges();
                }

                MessageBox.Show("Tarifa salva com sucesso!", "Tarifas");
            }
            catch (Exception)
            {
                db.Dispose();
                MessageBox.Show("Erro ao tentar salvar a Tarifa!", "Tarifas");
            }
        }
        private void CarregandoDados()
        {
            using (db = new DbSqlContext())
            {
                DataGridViewImageColumn img = new DataGridViewImageColumn();

                _pagamentoTarifasRepo = new PagamentoTarifasRepository(db);

                var dadosDbPagamento = _pagamentoTarifasRepo.SelecionarTodosPagamentos();
                var pagamentos = PagamentosTarifaMapper.Mapear(dadosDbPagamento);

                dataGridViewRelatorios.DataSource = pagamentos;
            }

            foreach (DataGridViewColumn column in dataGridViewRelatorios.Columns)
            {
                dataGridViewRelatorios.Columns[column.Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = dataGridViewRelatorios.Columns[column.Index].Width;
                dataGridViewRelatorios.Columns[column.Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewRelatorios.Columns[column.Index].Width = widthCol + 30;
            }
        }
    }
}
