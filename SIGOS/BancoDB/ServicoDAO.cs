using MySql.Data.MySqlClient;
using SIGOS.Classes;
using System;
using System.Data;

namespace SIGOS.BancoDB
{
    public class ServicoDAO
    {
        public DataTable Listar(string filtro)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT id, descricao, preco
                               FROM servicos
                               WHERE ativo = 1 AND descricao LIKE @f
                               ORDER BY descricao";
                var da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Inserir(Servico s)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(
                    "INSERT INTO servicos (descricao, preco) VALUES (@desc, @preco)", conn))
                {
                    cmd.Parameters.AddWithValue("@desc", s.Descricao.Trim());
                    cmd.Parameters.AddWithValue("@preco", s.Preco);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Servico s)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(
                    "UPDATE servicos SET descricao=@desc, preco=@preco WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@desc", s.Descricao.Trim());
                    cmd.Parameters.AddWithValue("@preco", s.Preco);
                    cmd.Parameters.AddWithValue("@id", s.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Retorna true se excluiu de verdade; false se só inativou (já usado em OS).
        public bool Excluir(int id)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();

                int usos;
                using (var cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM itens_os WHERE servico_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    usos = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (usos > 0)
                {
                    using (var cmd = new MySqlCommand(
                        "UPDATE servicos SET ativo = 0 WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return false;
                }

                using (var cmd = new MySqlCommand("DELETE FROM servicos WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
    }
}