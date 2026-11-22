using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmConsultaOrdemServico : Form
    {
        private readonly RelatorioDAO dao = new RelatorioDAO();
        private static readonly CultureInfo ptBR = new CultureInfo("pt-BR");

        public FrmConsultaOrdemServico()
        {
            InitializeComponent();

            // Eventos ligados aqui (não pelo designer). Não dê duplo clique nos controles.
            Load += FrmConsultaOrdemServico_Load;
            chkPeriodo.CheckedChanged += chkPeriodo_CheckedChanged;
            btnPesquisar.Click += btnPesquisar_Click;
            btnLimpar.Click += btnLimpar_Click;
            btnVoltar.Click += btnVoltar_Click;
            dgvResultado.CellFormatting += dgvResultado_CellFormatting;
        }

        private void FrmConsultaOrdemServico_Load(object sender, EventArgs e)
        {
            colTotal.DefaultCellStyle.Format = "N2";
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAbertura.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            CarregarFiltros();
            RestaurarDatas();
            Pesquisar();
        }

        private void CarregarFiltros()
        {
            try
            {
                // Status: o texto na tela é amigável, o valor ("Key") é o que está no banco
                var status = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", "Todos"),
                    new KeyValuePair<string, string>(FluxoStatus.Aberta, FluxoStatus.Descricao(FluxoStatus.Aberta)),
                    new KeyValuePair<string, string>(FluxoStatus.Diagnostico, FluxoStatus.Descricao(FluxoStatus.Diagnostico)),
                    new KeyValuePair<string, string>(FluxoStatus.AguardandoAprovacao, FluxoStatus.Descricao(FluxoStatus.AguardandoAprovacao)),
                    new KeyValuePair<string, string>(FluxoStatus.EmExecucao, FluxoStatus.Descricao(FluxoStatus.EmExecucao)),
                    new KeyValuePair<string, string>(FluxoStatus.Concluida, FluxoStatus.Descricao(FluxoStatus.Concluida)),
                    new KeyValuePair<string, string>(FluxoStatus.Cancelada, FluxoStatus.Descricao(FluxoStatus.Cancelada))
                };
                cbStatus.DisplayMember = "Value";
                cbStatus.ValueMember = "Key";
                cbStatus.DataSource = status;
                cbStatus.SelectedIndex = 0;

                // Cliente e técnico: primeira linha "Todos" (id vazio = sem filtro)
                DataTable clientes = dao.ListarClientesFiltro();
                DataRow todosCli = clientes.NewRow();
                todosCli["nome"] = "Todos";
                clientes.Rows.InsertAt(todosCli, 0);
                cbCliente.DisplayMember = "nome";
                cbCliente.ValueMember = "id";
                cbCliente.DataSource = clientes;
                cbCliente.SelectedIndex = 0;

                DataTable tecnicos = dao.ListarTecnicosFiltro();
                DataRow todosTec = tecnicos.NewRow();
                todosTec["nome"] = "Todos";
                tecnicos.Rows.InsertAt(todosTec, 0);
                cbTecnico.DisplayMember = "nome";
                cbTecnico.ValueMember = "id";
                cbTecnico.DataSource = tecnicos;
                cbTecnico.SelectedIndex = 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao carregar os filtros: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestaurarDatas()
        {
            DateTime hoje = DateTime.Today;
            dtpAte.Value = hoje;
            dtpDe.Value = new DateTime(hoje.Year, hoje.Month, 1);   // 1º dia do mês
        }

        private void chkPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            dtpDe.Enabled = chkPeriodo.Checked;
            dtpAte.Enabled = chkPeriodo.Checked;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            cbStatus.SelectedIndex = 0;
            cbCliente.SelectedIndex = 0;
            cbTecnico.SelectedIndex = 0;
            chkPeriodo.Checked = false;
            RestaurarDatas();
            Pesquisar();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Pesquisar()
        {
            string status = cbStatus.SelectedValue as string;
            if (string.IsNullOrEmpty(status)) status = null;

            int? clienteId = ComoInt(cbCliente.SelectedValue);
            int? tecnicoId = ComoInt(cbTecnico.SelectedValue);

            DateTime? de = null;
            DateTime? ate = null;
            if (chkPeriodo.Checked)
            {
                if (dtpDe.Value.Date > dtpAte.Value.Date)
                {
                    MessageBox.Show("A data inicial não pode ser maior que a data final.");
                    dtpDe.Focus();
                    return;
                }
                de = dtpDe.Value.Date;
                ate = dtpAte.Value.Date.AddDays(1);   // inclui o dia final inteiro
            }

            try
            {
                DataTable dados = dao.ConsultarOS(status, clienteId, tecnicoId, de, ate);
                dgvResultado.DataSource = dados;
                MostrarResumo(dados);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao consultar as ordens de serviço: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarResumo(DataTable dados)
        {
            object soma = dados.Compute("SUM(total)", "status <> 'CANCELADA'");
            decimal total = soma == DBNull.Value ? 0m : Convert.ToDecimal(soma);

            lblResumo.Text = dados.Rows.Count + " ordem(ns) encontrada(s)   |   " +
                             "Total (exceto canceladas): R$ " + total.ToString("N2", ptBR);
        }

        private void dgvResultado_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == colTecnico.Index && (e.Value == null || e.Value == DBNull.Value))
            {
                e.Value = "(sem técnico)";
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == colStatus.Index && e.Value != null)
            {
                e.Value = FluxoStatus.Descricao(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private static int? ComoInt(object valor)
        {
            int n;
            if (valor != null && int.TryParse(Convert.ToString(valor), out n))
                return n;
            return null;
        }
    }
}