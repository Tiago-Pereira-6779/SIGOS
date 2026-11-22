using MySql.Data.MySqlClient;
using SIGOS.Classes;
using SIGOS.Segurança;
using System;

namespace SIGOS.BancoDB
{
    public class UsuarioDAO
    {
        // Retorna o usuário se login e senha conferem; senão retorna null.
        public Usuario Autenticar(string login, string senha)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT id, nome, login, senha_hash, perfil
                               FROM usuarios
                               WHERE login = @login AND ativo = 1";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login.Trim());
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) return null;

                        string hash = rd.GetString("senha_hash");
                        if (!SenhaHelper.Verificar(senha, hash)) return null;

                        return new Usuario
                        {
                            Id = rd.GetInt32("id"),
                            Nome = rd.GetString("nome"),
                            Login = rd.GetString("login"),
                            Perfil = rd.GetString("perfil")
                        };
                    }
                }
            }
        }

        public bool ExisteUsuario()
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios", conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // perfil deve ser ADMIN, ATENDENTE ou TECNICO (valores do ENUM no banco)
        public void Inserir(string nome, string login, string senha, string perfil)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"INSERT INTO usuarios (nome, login, senha_hash, perfil)
                               VALUES (@nome, @login, @hash, @perfil)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    cmd.Parameters.AddWithValue("@login", login.Trim());
                    cmd.Parameters.AddWithValue("@hash", SenhaHelper.GerarHash(senha));
                    cmd.Parameters.AddWithValue("@perfil", perfil);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Primeiro acesso: se a tabela estiver vazia, cria o administrador inicial.
        public void CriarAdminInicial()
        {
            if (!ExisteUsuario())
                Inserir("Administrador", "admin", "admin123", "ADMIN");
        }
    }
}