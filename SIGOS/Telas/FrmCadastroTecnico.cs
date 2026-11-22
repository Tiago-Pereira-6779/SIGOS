using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using SIGOS.Seguranca;
using System;
using System.Data;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmCadastroTecnico : Form
    {
        private readonly TecnicoDAO dao = new TecnicoDAO();

        public FrmCadastroTecnico()
        {
            InitializeComponent();
        }

        private void FrmCadastroTecnico_Load(object sender, EventArgs e)
        {
            dgvTecnicos.AutoGenerateColumns = false;
            CarregarGrid();
            LimparCampos();
        }

        private void CarregarGrid()
        {
            try
            {
                dgvTecnicos.DataSource = dao.Listar(txtPesquisa.Text.Trim());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao consultar técnicos: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            txtEspecialidade.Clear();
            dgvTecnicos.ClearSelection();
            txtNome.Focus();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void dgvTecnicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataRow linha = ((DataRowView)dgvTecnicos.Rows[e.RowIndex].DataBoundItem).Row;

            txtId.Text = linha["id"].ToString();
            txtNome.Text = linha["nome"].ToString();
            txtEspecialidade.Text = linha["especialidade"].ToString();
            txtTelefone.Text = linha["telefone"].ToString();
            txtEmail.Text = linha["email"].ToString();
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
                MessageBox.Show("Selecione um técnico na lista para editar.");
                return;
            }
            txtNome.Focus(); // altere os campos e clique em Salvar
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string telefone = Validador.SomenteNumeros(txtTelefone.Text);

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do técnico.");
                txtNome.Focus();
                return;
            }
            if (telefone.Length > 0 && !Validador.TelefoneValido(telefone))
            {
                MessageBox.Show("Telefone inválido. Informe DDD + número.");
                txtTelefone.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("E-mail inválido.");
                txtEmail.Focus();
                return;
            }

            var t = new Tecnico
            {
                Nome = txtNome.Text,
                Especialidade = txtEspecialidade.Text,
                Telefone = telefone,
                Email = txtEmail.Text
            };

            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    dao.Inserir(t);
                    MessageBox.Show("Técnico cadastrado com sucesso!");
                }
                else
                {
                    t.Id = int.Parse(txtId.Text);
                    dao.Atualizar(t);
                    MessageBox.Show("Técnico atualizado com sucesso!");
                }
                CarregarGrid();
                LimparCampos();
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
                MessageBox.Show("Selecione um técnico na lista.");
                return;
            }

            if (MessageBox.Show("Deseja excluir este técnico?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bool excluido = dao.Excluir(int.Parse(txtId.Text));

                MessageBox.Show(excluido
                    ? "Técnico excluído com sucesso."
                    : "Este técnico possui ordens de serviço vinculadas, então foi inativado em vez de excluído.");

                CarregarGrid();
                LimparCampos();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}