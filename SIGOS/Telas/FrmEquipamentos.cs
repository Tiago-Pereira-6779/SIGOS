using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmEquipamentos : Form
    {
        private readonly EquipamentoDAO dao = new EquipamentoDAO();

        public FrmEquipamentos()
        {
            InitializeComponent();
        }

        private void FrmEquipamentos_Load(object sender, EventArgs e)
        {
            dgvEquipamentos.AutoGenerateColumns = false;
            CarregarClientes();
            CarregarGrid();
            LimparCampos();
        }

        // Preenche o ComboBox com os clientes do banco
        private void CarregarClientes()
        {
            try
            {
                DataTable clientes = new ClienteDAO().Listar("");
                cbCliente.DisplayMember = "nome";
                cbCliente.ValueMember = "id";
                cbCliente.DataSource = clientes;
                cbCliente.SelectedIndex = -1;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarGrid()
        {
            try
            {
                dgvEquipamentos.DataSource = dao.Listar(txtPesquisa.Text.Trim());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao consultar equipamentos: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            cbCliente.SelectedIndex = -1;
            txtTipo.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtNumeroSerie.Clear();
            txtDefeito.Clear();
            dgvEquipamentos.ClearSelection();
            cbCliente.Focus();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void dgvEquipamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataRow linha = ((DataRowView)dgvEquipamentos.Rows[e.RowIndex].DataBoundItem).Row;

            txtId.Text = linha["id"].ToString();
            cbCliente.SelectedValue = Convert.ToInt32(linha["cliente_id"]);
            txtTipo.Text = linha["tipo"].ToString();
            txtMarca.Text = linha["marca"].ToString();
            txtModelo.Text = linha["modelo"].ToString();
            txtNumeroSerie.Text = linha["numero_serie"].ToString();
            txtDefeito.Text = linha["defeito_informado"].ToString();
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
                MessageBox.Show("Selecione um equipamento na lista para editar.");
                return;
            }
            txtTipo.Focus(); // altere os campos e clique em Salvar
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedValue == null)
            {
                MessageBox.Show("Selecione o cliente proprietário do equipamento.");
                cbCliente.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                MessageBox.Show("Informe o tipo do equipamento (ex.: Notebook).");
                txtTipo.Focus();
                return;
            }
            if (txtTipo.Text.Trim().Length > 60 || txtMarca.Text.Trim().Length > 60)
            {
                MessageBox.Show("Tipo e marca podem ter no máximo 60 caracteres.");
                return;
            }
            if (txtModelo.Text.Trim().Length > 80)
            {
                MessageBox.Show("O modelo pode ter no máximo 80 caracteres.");
                txtModelo.Focus();
                return;
            }
            if (txtNumeroSerie.Text.Trim().Length > 100)
            {
                MessageBox.Show("O número de série pode ter no máximo 100 caracteres.");
                txtNumeroSerie.Focus();
                return;
            }

            var eq = new Equipamento
            {
                ClienteId = Convert.ToInt32(cbCliente.SelectedValue),
                Tipo = txtTipo.Text,
                Marca = txtMarca.Text,
                Modelo = txtModelo.Text,
                NumeroSerie = txtNumeroSerie.Text,
                DefeitoInformado = txtDefeito.Text
            };

            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    dao.Inserir(eq);
                    MessageBox.Show("Equipamento cadastrado com sucesso!");
                }
                else
                {
                    eq.Id = int.Parse(txtId.Text);
                    dao.Atualizar(eq);
                    MessageBox.Show("Equipamento atualizado com sucesso!");
                }
                CarregarGrid();
                LimparCampos();
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                // trocar o dono de um equipamento que já tem OS quebraria a chave composta
                MessageBox.Show("Este equipamento já possui ordens de serviço, então não é possível trocar o cliente proprietário.");
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
                MessageBox.Show("Selecione um equipamento na lista.");
                return;
            }

            if (MessageBox.Show("Deseja excluir este equipamento?", "Confirmação",
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
                MessageBox.Show(ex.Message); // equipamento com OS vinculada
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

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um equipamento na lista para ver o histórico.");
                return;
            }

            string descricao = cbCliente.Text + " - " + txtTipo.Text + " " +
                               txtMarca.Text + " " + txtModelo.Text;

            using (var tela = new FrmHistoricoEquipamento(int.Parse(txtId.Text), descricao.Trim()))
            {
                tela.ShowDialog(this);
            }
        }
    }
}