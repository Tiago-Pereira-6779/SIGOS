using MySql.Data.MySqlClient;
using SIGOS.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGOS.BancoDB
{
    public class ClienteDAO
    {
        public DataTable Listar(string filtro)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT id, nome, documento, telefone, email, endereco
                               FROM clientes
                               WHERE ativo = 1 AND (nome LIKE @f OR documento LIKE @f)
                               ORDER BY nome";
                var da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Inserir(Cliente c)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"INSERT INTO clientes (nome, documento, telefone, email, endereco)
                               VALUES (@nome, @doc, @tel, @email, @end)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    Preencher(cmd, c);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Cliente c)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"UPDATE clientes
                               SET nome=@nome, documento=@doc, telefone=@tel,
                                   email=@email, endereco=@end
                               WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    Preencher(cmd, c);
                    cmd.Parameters.AddWithValue("@id", c.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();

                using (var cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM ordens_servico WHERE cliente_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        throw new InvalidOperationException(
                            "Este cliente possui ordens de serviço vinculadas e não pode ser excluído.");
                }

                using (var cmd = new MySqlCommand("DELETE FROM clientes WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Preencher(MySqlCommand cmd, Cliente c)
        {
            cmd.Parameters.AddWithValue("@nome", c.Nome.Trim());
            cmd.Parameters.AddWithValue("@doc", c.Documento);
            cmd.Parameters.AddWithValue("@tel", Nulo(c.Telefone));
            cmd.Parameters.AddWithValue("@email", Nulo(c.Email));
            cmd.Parameters.AddWithValue("@end", Nulo(c.Endereco));
        }

        private object Nulo(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor.Trim();
        }

    }
}
