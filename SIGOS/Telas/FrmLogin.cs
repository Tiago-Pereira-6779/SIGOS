using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Windows.Forms;

namespace SIGOS
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioDAO dao = new UsuarioDAO();
        private const int MaxTentativas = 5;
        private int tentativas = 0;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string login = txtUsuario.Text.Trim();
            string senha = txtSenha.Text;

            if (login.Length == 0 || senha.Length == 0)
            {
                MessageBox.Show("Informe o usuário e a senha.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Usuario usuario = dao.Autenticar(login, senha);

                if (usuario == null)
                {
                    tentativas++;

                    if (tentativas >= MaxTentativas)
                    {
                        MessageBox.Show("Número máximo de tentativas excedido. O sistema será fechado.",
                            "Acesso bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        DialogResult = DialogResult.Cancel;   // fecha a tela de login
                        return;
                    }

                    // mesma mensagem para usuário inexistente e senha errada
                    MessageBox.Show("Usuário ou senha inválidos.", "Falha no login",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenha.Clear();
                    txtSenha.Focus();
                    return;
                }

                Sessao.UsuarioLogado = usuario;
                DialogResult = DialogResult.OK;               // fecha a tela de login
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Não foi possível acessar o banco de dados.\n" +
                    "Verifique se o MySQL do XAMPP está iniciado.\n\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}