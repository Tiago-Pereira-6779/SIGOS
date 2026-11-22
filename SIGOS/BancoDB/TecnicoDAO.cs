using MySql.Data.MySqlClient;
using SIGOS.Classes;
using System;
using System.Data;

namespace SIGOS.BancoDB
{
    public class TecnicoDAO
    {
        public DataTable Listar(string filtro)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT id, nome, especialidade, telefone, email
                               FROM tecnicos
                               WHERE ativo = 1 AND (nome LIKE @f OR especialidade LIKE @f)
                               ORDER BY nome";
                var da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Inserir(Tecnico t)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"INSERT INTO tecnicos (nome, especialidade, telefone, email)
                               VALUES (@nome, @esp, @tel, @email)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    Preencher(cmd, t);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Tecnico t)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"UPDATE tecnicos
                               SET nome=@nome, especialidade=@esp, telefone=@tel, email=@email
                               WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    Preencher(cmd, t);
                    cmd.Parameters.AddWithValue("@id", t.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Retorna true se excluiu de verdade; false se só inativou (tem OS vinculada).
        public bool Excluir(int id)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();

                int qtdOs;
                using (var cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM ordens_servico WHERE tecnico_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    qtdOs = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (qtdOs > 0)
                {
                    using (var cmd = new MySqlCommand(
                        "UPDATE tecnicos SET ativo = 0 WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return false;
                }

                using (var cmd = new MySqlCommand("DELETE FROM tecnicos WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }

        private void Preencher(MySqlCommand cmd, Tecnico t)
        {
            cmd.Parameters.AddWithValue("@nome", t.Nome.Trim());
            cmd.Parameters.AddWithValue("@esp", Nulo(t.Especialidade));
            cmd.Parameters.AddWithValue("@tel", Nulo(t.Telefone));
            cmd.Parameters.AddWithValue("@email", Nulo(t.Email));
        }

        private object Nulo(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor.Trim();
        }
    }
}