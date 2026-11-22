using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmRelatorios : Form
    {
        private readonly RelatorioDAO dao = new RelatorioDAO();
        private static readonly CultureInfo ptBR = new CultureInfo("pt-BR");

        public FrmRelatorios()
        {
            InitializeComponent();

            // Eventos ligados aqui (não pelo designer). Não dê duplo clique nos controles.
            Load += FrmRelatorios_Load;
            btnAtualizar.Click += btnAtualizar_Click;
            btnVoltar.Click += btnVoltar_Click;
            dgvStatus.CellFormatting += dgvStatus_CellFormatting;
            dgvPendentes.CellFormatting += dgvPendentes_CellFormatting;
        }

        private void FrmRelatorios_Load(object sender, EventArgs e)
        {
            FormatarGrids();
            Atualizar();
        }

        private void FormatarGrids()
        {
            DataGridViewContentAlignment direita = DataGridViewContentAlignment.MiddleRight;

            colStQtd.DefaultCellStyle.Alignment = direita;
            colStValor.DefaultCellStyle.Format = "N2";
            colStValor.DefaultCellStyle.Alignment = direita;

            colTcAndamento.DefaultCellStyle.Alignment = direita;
            colTcConcluidas.DefaultCellStyle.Alignment = direita;
            colTcFaturado.DefaultCellStyle.Format = "N2";
            colTcFaturado.DefaultCellStyle.Alignment = direita;

            colPdAbertura.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            colPdTotal.DefaultCellStyle.Format = "N2";
            colPdTotal.DefaultCellStyle.Alignment = direita;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Lê tudo de novo do banco e redesenha o painel
        private void Atualizar()
        {
            try
            {
                DataRow ind = dao.Indicadores().Rows[0];

                lblValTotal.Text = Convert.ToInt32(ind["total_os"]).ToString();
                lblValPendentes.Text = Convert.ToInt32(ind["pendentes"]).ToString();
                lblValEquipamentos.Text = Convert.ToInt32(ind["equipamentos"]).ToString();
                lblValPrevisto.Text = Moeda(Convert.ToDecimal(ind["previsto"]));
                lblValFaturado.Text = Moeda(Convert.ToDecimal(ind["faturado"]));

                dgvStatus.DataSource = dao.ResumoPorStatus();
                dgvTecnicos.DataSource = dao.ResumoPorTecnico();
                dgvPendentes.DataSource = dao.ListarPendentes();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao carregar o painel: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            if (e.ColumnIndex == colStStatus.Index)
            {
                e.Value = FluxoStatus.Descricao(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private void dgvPendentes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == colPdTecnico.Index && (e.Value == null || e.Value == DBNull.Value))
            {
                e.Value = "(sem técnico)";
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == colPdStatus.Index && e.Value != null)
            {
                e.Value = FluxoStatus.Descricao(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private static string Moeda(decimal valor)
        {
            return "R$ " + valor.ToString("N2", ptBR);
        }
    }
}