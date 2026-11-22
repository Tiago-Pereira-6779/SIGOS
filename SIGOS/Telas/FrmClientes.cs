using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using SIGOS.Seguranca;
using System;
using System.Data;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmClientes : Form
    {
        private readonly ClienteDAO dao = new ClienteDAO();

        // A tela não tem campo de endereço. Guardamos o valor atual do cliente
        // selecionado para que o UPDATE não apague o endereço dele no banco.
        private string enderecoSelecionado = null;

        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            dgvClientes.AutoGenerateColumns = false; // usa só as colunas do designer
            CarregarGrid();
            LimparCampos();
        }

        private void CarregarGrid()
        {
            try
            {
                dgvClientes.DataSource = dao.Listar(txtPesquisa.Text.Trim());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao consultar clientes: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtDocumento.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            enderecoSelecionado = null;
            dgvClientes.ClearSelection();
            txtNome.Focus();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lê direto da linha do banco, assim alcança email e endereco
            // mesmo que não existam como colunas no grid.
            DataRow linha = ((DataRowView)dgvClientes.Rows[e.RowIndex].DataBoundItem).Row;

            txtId.Text = linha["id"].ToString();
            txtNome.Text = linha["nome"].ToString();
            txtDocumento.Text = linha["documento"].ToString();
            txtTelefone.Text = linha["telefone"].ToString();
            txtEmail.Text = linha["email"].ToString();
            enderecoSelecionado = linha["endereco"] as string;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisa.Clear();   // dispara o TextChanged e recarrega o grid
            LimparCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um cliente na lista para editar.");
                return;
            }
            txtNome.Focus(); // altere os campos e clique em Salvar
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string telefone = Validador.SomenteNumeros(txtTelefone.Text);

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do cliente.");
                txtNome.Focus();
                return;
            }
            if (!Validador.DocumentoValido(txtDocumento.Text))
            {
                MessageBox.Show("CPF/CNPJ inválido.");
                txtDocumento.Focus();
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

            var c = new Cliente
            {
                Nome = txtNome.Text,
                Documento = Validador.SomenteNumeros(txtDocumento.Text),
                Telefone = telefone,
                Email = txtEmail.Text,
                Endereco = enderecoSelecionado
            };

            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    dao.Inserir(c);
                    MessageBox.Show("Cliente cadastrado com sucesso!");
                }
                else
                {
                    c.Id = int.Parse(txtId.Text);
                    dao.Atualizar(c);
                    MessageBox.Show("Cliente atualizado com sucesso!");
                }
                CarregarGrid();
                LimparCampos();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Já existe um cliente com este CPF/CNPJ.");
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
                MessageBox.Show("Selecione um cliente na lista.");
                return;
            }

            if (MessageBox.Show("Deseja excluir este cliente?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                dao.Excluir(int.Parse(txtId.Text));
                CarregarGrid();
                LimparCampos();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message); // cliente com OS vinculada
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                MessageBox.Show("Este cliente possui equipamentos cadastrados e não pode ser excluído.");
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