using MySql.Data.MySqlClient;
using System.Data;

namespace SIGOS.BancoDB
{
    // Histórico de um equipamento = todas as OS que já foram abertas para ele.
    public class HistoricoEquipamentoDAO
    {
        public DataTable Listar(int equipamentoId)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT os.id AS numero_os, os.data_abertura, os.data_conclusao,
                                      os.status, os.prioridade, t.nome AS tecnico,
                                      os.descricao_problema, os.diagnostico, os.solucao, os.total
                               FROM ordens_servico os
                               LEFT JOIN tecnicos t ON t.id = os.tecnico_id
                               WHERE os.equipamento_id = @id
                               ORDER BY os.id DESC";

                var da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", equipamentoId);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}