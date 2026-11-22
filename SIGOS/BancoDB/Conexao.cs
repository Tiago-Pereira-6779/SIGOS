using MySql.Data.MySqlClient;

namespace SIGOS.BancoDB
{
    public static class Conexao
    {
        private const string ConnectionString =
            "Server=localhost;Port=3306;Database=sigosdb;Uid=root;Pwd=;CharSet=utf8mb4;";

        public static MySqlConnection ObterConexao()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static bool Testar(out string mensagem)
        {
            try
            {
                using (var conn = ObterConexao())
                {
                    conn.Open();
                    mensagem = "Conexão realizada com sucesso!";
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                mensagem = "Erro ao conectar: " + ex.Message;
                return false;
            }
        }
    }
}