using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SIGOS.BancoDB
{
    // Consultas de leitura usadas pela Consulta de OS e pelo Painel de indicadores.
    public class RelatorioDAO
    {
        // CONSULTA DE OS (filtros opcionais)

        // O que vier null não filtra.
        // ateExclusivo = dia seguinte à data final (assim a data final entra inteira no resultado).
        public DataTable ConsultarOS(string status, int? clienteId, int? tecnicoId,
                                     DateTime? de, DateTime? ateExclusivo)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT numero_os, data_abertura, cliente, equipamento, tecnico,
                                      status, prioridade, total
                               FROM vw_os_detalhada
                               WHERE (@status IS NULL OR status = @status)
                                 AND (@cli IS NULL OR cliente_id = @cli)
                                 AND (@tec IS NULL OR tecnico_id = @tec)
                                 AND (@de IS NULL OR data_abertura >= @de)
                                 AND (@ate IS NULL OR data_abertura < @ate)
                               ORDER BY numero_os DESC";

                var da = new MySqlDataAdapter(sql, conn);
                var p = da.SelectCommand.Parameters;
                p.AddWithValue("@status", status == null ? (object)DBNull.Value : status);
                p.AddWithValue("@cli", clienteId.HasValue ? (object)clienteId.Value : DBNull.Value);
                p.AddWithValue("@tec", tecnicoId.HasValue ? (object)tecnicoId.Value : DBNull.Value);
                p.AddWithValue("@de", de.HasValue ? (object)de.Value : DBNull.Value);
                p.AddWithValue("@ate", ateExclusivo.HasValue ? (object)ateExclusivo.Value : DBNull.Value);

                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Para os combos de filtro: inclui também os inativos, porque eles podem ter OS antigas.
        public DataTable ListarClientesFiltro()
        {
            return Consultar("SELECT id, nome FROM clientes ORDER BY nome");
        }

        public DataTable ListarTecnicosFiltro()
        {
            return Consultar("SELECT id, nome FROM tecnicos ORDER BY nome");
        }

        // PAINEL DE INDICADORES

        // Uma única linha com os números dos cartões.
        // "Em andamento" = qualquer status que não seja CONCLUIDA nem CANCELADA.
        public DataTable Indicadores()
        {
            string sql = @"SELECT
                               COUNT(*) AS total_os,
                               COALESCE(SUM(CASE WHEN status NOT IN ('CONCLUIDA','CANCELADA')
                                                 THEN 1 ELSE 0 END), 0) AS pendentes,
                               COUNT(DISTINCT CASE WHEN status NOT IN ('CONCLUIDA','CANCELADA')
                                                   THEN equipamento_id END) AS equipamentos,
                               COALESCE(SUM(CASE WHEN status NOT IN ('CONCLUIDA','CANCELADA')
                                                 THEN total ELSE 0 END), 0) AS previsto,
                               COALESCE(SUM(CASE WHEN status = 'CONCLUIDA'
                                                 THEN total ELSE 0 END), 0) AS faturado
                           FROM ordens_servico";
            return Consultar(sql);
        }

        public DataTable ResumoPorStatus()
        {
            string sql = @"SELECT status, COUNT(*) AS quantidade, COALESCE(SUM(total), 0) AS valor_total
                           FROM ordens_servico
                           GROUP BY status
                           ORDER BY FIELD(status, 'ABERTA', 'DIAGNOSTICO', 'AGUARDANDO_APROVACAO',
                                                  'EM_EXECUCAO', 'CONCLUIDA', 'CANCELADA')";
            return Consultar(sql);
        }

        public DataTable ResumoPorTecnico()
        {
            string sql = @"SELECT COALESCE(t.nome, '(sem técnico)') AS tecnico,
                                  SUM(CASE WHEN os.status NOT IN ('CONCLUIDA','CANCELADA')
                                           THEN 1 ELSE 0 END) AS em_andamento,
                                  SUM(CASE WHEN os.status = 'CONCLUIDA' THEN 1 ELSE 0 END) AS concluidas,
                                  COALESCE(SUM(CASE WHEN os.status = 'CONCLUIDA'
                                                    THEN os.total ELSE 0 END), 0) AS faturado
                           FROM ordens_servico os
                           LEFT JOIN tecnicos t ON t.id = os.tecnico_id
                           GROUP BY os.tecnico_id, t.nome
                           ORDER BY em_andamento DESC, tecnico";
            return Consultar(sql);
        }

        // OS ainda não finalizadas: são os serviços pendentes e os equipamentos em manutenção.
        // Mais urgentes primeiro, depois as mais antigas.
        public DataTable ListarPendentes()
        {
            string sql = @"SELECT numero_os, cliente, equipamento, tecnico, status, data_abertura, total
                           FROM vw_os_detalhada
                           WHERE status NOT IN ('CONCLUIDA','CANCELADA')
                           ORDER BY FIELD(prioridade, 'URGENTE', 'ALTA', 'MEDIA', 'BAIXA'), data_abertura";
            return Consultar(sql);
        }

        private DataTable Consultar(string sql)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                var da = new MySqlDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}