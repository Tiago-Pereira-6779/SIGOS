using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmCadastroServicos : Form
    {
        private readonly ServicoDAO dao = new ServicoDAO();
        private static readonly CultureInfo ptBR = new CultureInfo("pt-BR");

        public FrmCadastroServicos()
        {
            InitializeComponent();
        }

        private void FrmCadastroServicos_Load(object sender, EventArgs e)
        {
            dgvServicos.AutoGenerateColumns = false;
            Valor.DefaultCellStyle.Format = "N2";   // 120,00
            Valor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            CarregarGrid();
            LimparCampos();
        }

        private void CarregarGrid()
        {
            try
            {
                dgvServicos.DataSource = dao.Listar(txtPesquisa.Text.Trim());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao consultar serviços: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtDescricao.Clear();
            txtValor.Clear();
            dgvServicos.ClearSelection();
            txtDescricao.Focus();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void dgvServicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataRow linha = ((DataRowView)dgvServicos.Rows[e.RowIndex].DataBoundItem).Row;

            txtId.Text = linha["id"].ToString();
            txtDescricao.Text = linha["descricao"].ToString();
            txtValor.Text = Convert.ToDecimal(linha["preco"]).ToString("N2", ptBR);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisa.Clear();
            LimparCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um serviço na lista para editar.");
                return;
            }
            txtDescricao.Focus(); // altere os campos e clique em Salvar
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text.Trim();

            if (descricao.Length == 0)
            {
                MessageBox.Show("Informe a descrição do serviço.");
                txtDescricao.Focus();
                return;
            }
            if (descricao.Length > 120)
            {
                MessageBox.Show("A descrição pode ter no máximo 120 caracteres.");
                txtDescricao.Focus();
                return;
            }

            decimal preco;
            string textoValor = txtValor.Text.Replace("R$", "").Trim();
            if (!decimal.TryParse(textoValor, NumberStyles.Number, ptBR, out preco)
                || preco <= 0 || preco > 99999999.99m)
            {
                MessageBox.Show("Informe um valor válido, maior que zero (ex.: 120,50).");
                txtValor.Focus();
                return;
            }

            var s = new Servico
            {
                Descricao = descricao,
                Preco = Math.Round(preco, 2)
            };

            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    dao.Inserir(s);
                    MessageBox.Show("Serviço cadastrado com sucesso!");
                }
                else
                {
                    s.Id = int.Parse(txtId.Text);
                    dao.Atualizar(s);
                    MessageBox.Show("Serviço atualizado com sucesso!");
                }
                CarregarGrid();
                LimparCampos();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Já existe um serviço com esta descrição (ele pode estar inativo).");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um serviço na lista.");
                return;
            }

            if (MessageBox.Show("Deseja excluir este serviço?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bool excluido = dao.Excluir(int.Parse(txtId.Text));

                MessageBox.Show(excluido
                    ? "Serviço excluído com sucesso."
                    : "Este serviço já foi usado em ordens de serviço, então foi inativado em vez de excluído.");

                CarregarGrid();
                LimparCampos();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}