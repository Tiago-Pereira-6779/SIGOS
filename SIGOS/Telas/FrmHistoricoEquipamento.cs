using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    // Mostra todas as OS já abertas para UM equipamento (Módulo 2 do PDF).
    // Aberta pela tela de Equipamentos: new FrmHistoricoEquipamento(id, descricao)
    public partial class FrmHistoricoEquipamento : Form
    {
        private readonly HistoricoEquipamentoDAO dao = new HistoricoEquipamentoDAO();
        private readonly int equipamentoId;
        private readonly string descricaoEquipamento;

        public FrmHistoricoEquipamento(int equipamentoId, string descricaoEquipamento)
        {
            InitializeComponent();

            this.equipamentoId = equipamentoId;
            this.descricaoEquipamento = descricaoEquipamento;

            // Eventos ligados aqui (não pelo designer). Não dê duplo clique nos controles.
            Load += FrmHistoricoEquipamento_Load;
            dgvHistorico.SelectionChanged += dgvHistorico_SelectionChanged;
            dgvHistorico.CellFormatting += dgvHistorico_CellFormatting;
            btnFechar.Click += btnFechar_Click;
        }

        private void FrmHistoricoEquipamento_Load(object sender, EventArgs e)
        {
            colAbertura.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            colConclusao.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            colTotal.DefaultCellStyle.Format = "N2";
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            lblEquipamento.Text = descricaoEquipamento;
            Carregar();
        }

        private void Carregar()
        {
            try
            {
                DataTable historico = dao.Listar(equipamentoId);
                dgvHistorico.DataSource = historico;

                if (historico.Rows.Count == 0)
                {
                    lblResumo.Text = "Este equipamento ainda não tem ordens de serviço.";
                    txtDetalhes.Clear();
                }
                else
                {
                    lblResumo.Text = historico.Rows.Count + " ordem(ns) de serviço para este equipamento.";
                    MostrarDetalhes();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao consultar o histórico: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ao escolher uma linha, mostra o texto completo do problema, diagnóstico e solução
        private void dgvHistorico_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalhes();
        }

        private void MostrarDetalhes()
        {
            txtDetalhes.Clear();
            if (dgvHistorico.CurrentRow == null) return;

            DataRowView linha = dgvHistorico.CurrentRow.DataBoundItem as DataRowView;
            if (linha == null) return;

            txtDetalhes.Text =
                "Problema: " + Texto(linha["descricao_problema"]) + "\r\n\r\n" +
                "Diagnóstico: " + Texto(linha["diagnostico"]) + "\r\n\r\n" +
                "Solução aplicada: " + Texto(linha["solucao"]);
        }

        private void dgvHistorico_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string Texto(object valor)
        {
            if (valor == null || valor == DBNull.Value || string.IsNullOrWhiteSpace(valor.ToString()))
                return "(não informado)";
            return valor.ToString();
        }
    }
}