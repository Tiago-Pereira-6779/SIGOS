using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Telas;
using System;
using System.Windows.Forms;

namespace SIGOS
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1) Garante que existe ao menos um usuário (admin / admin123 no primeiro acesso)
            try
            {
                var usuarios = new UsuarioDAO();
                usuarios.CriarAdminInicial();

                // Para testar os perfis: tire o // das linhas abaixo, rode UMA vez
                // e volte a comentar (rodar de novo dá erro de login duplicado).
                // usuarios.Inserir("Atendente Teste", "atendente", "123456", "ATENDENTE");
                // usuarios.Inserir("Técnico Teste", "tecnico", "123456", "TECNICO");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Não foi possível conectar ao banco de dados.\n" +
                    "Verifique se o MySQL do XAMPP está iniciado.\n\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Tela de login: só segue para o menu se o login der certo
            using (var login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            // 3) Menu principal
            Application.Run(new FrmMenu());
        }
    }
}