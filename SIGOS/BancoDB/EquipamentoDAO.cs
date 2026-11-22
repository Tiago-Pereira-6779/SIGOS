using MySql.Data.MySqlClient;
using SIGOS.Classes;
using System;
using System.Data;

namespace SIGOS.BancoDB
{
    public class EquipamentoDAO
    {
        public DataTable Listar(string filtro)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT e.id, e.cliente_id, c.nome AS cliente, e.tipo, e.marca,
                                      e.modelo, e.numero_serie, e.defeito_informado
                               FROM equipamentos e
                               JOIN clientes c ON c.id = e.cliente_id
                               WHERE c.nome LIKE @f OR e.tipo LIKE @f OR e.marca LIKE @f
                                  OR e.modelo LIKE @f OR e.numero_serie LIKE @f
                               ORDER BY c.nome, e.tipo";
                var da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Inserir(Equipamento eq)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"INSERT INTO equipamentos
                                   (cliente_id, tipo, marca, modelo, numero_serie, defeito_informado)
                               VALUES (@cli, @tipo, @marca, @modelo, @serie, @defeito)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    Preencher(cmd, eq);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Equipamento eq)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"UPDATE equipamentos
                               SET cliente_id=@cli, tipo=@tipo, marca=@marca, modelo=@modelo,
                                   numero_serie=@serie, defeito_informado=@defeito
                               WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    Preencher(cmd, eq);
                    cmd.Parameters.AddWithValue("@id", eq.Id);
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
                    "SELECT COUNT(*) FROM ordens_servico WHERE equipamento_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        throw new InvalidOperationException(
                            "Este equipamento possui ordens de serviço vinculadas e não pode ser excluído.");
                }

                using (var cmd = new MySqlCommand("DELETE FROM equipamentos WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Preencher(MySqlCommand cmd, Equipamento eq)
        {
            cmd.Parameters.AddWithValue("@cli", eq.ClienteId);
            cmd.Parameters.AddWithValue("@tipo", eq.Tipo.Trim());
            cmd.Parameters.AddWithValue("@marca", Nulo(eq.Marca));
            cmd.Parameters.AddWithValue("@modelo", Nulo(eq.Modelo));
            cmd.Parameters.AddWithValue("@serie", Nulo(eq.NumeroSerie));
            cmd.Parameters.AddWithValue("@defeito", Nulo(eq.DefeitoInformado));
        }

        private object Nulo(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor.Trim();
        }
    }
}