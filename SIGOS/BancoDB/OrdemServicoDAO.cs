using MySql.Data.MySqlClient;
using SIGOS.Classes;
using System;
using System.Data;

namespace SIGOS.BancoDB
{
    public class OrdemServicoDAO
    {
        // CONSULTAS

        public DataTable Listar(string filtro)
        {
            string sql = @"SELECT numero_os, cliente, equipamento, tecnico, status,
                                  prioridade, total, data_abertura
                           FROM vw_os_detalhada
                           WHERE CAST(numero_os AS CHAR) LIKE @f
                              OR cliente LIKE @f OR equipamento LIKE @f
                           ORDER BY numero_os DESC";
            return Consultar(sql, "@f", "%" + filtro + "%");
        }

        public OrdemServico Buscar(int id)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"SELECT id, cliente_id, equipamento_id, tecnico_id, prioridade, status,
                                      descricao_problema, diagnostico, solucao,
                                      subtotal, desconto, total, data_abertura
                               FROM ordens_servico WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) return null;

                        return new OrdemServico
                        {
                            Id = rd.GetInt32("id"),
                            ClienteId = rd.GetInt32("cliente_id"),
                            EquipamentoId = rd.GetInt32("equipamento_id"),
                            TecnicoId = rd["tecnico_id"] == DBNull.Value
                                            ? (int?)null : Convert.ToInt32(rd["tecnico_id"]),
                            Prioridade = rd.GetString("prioridade"),
                            Status = rd.GetString("status"),
                            DescricaoProblema = rd.GetString("descricao_problema"),
                            Diagnostico = rd["diagnostico"] as string,
                            Solucao = rd["solucao"] as string,
                            Subtotal = rd.GetDecimal("subtotal"),
                            Desconto = rd.GetDecimal("desconto"),
                            Total = rd.GetDecimal("total"),
                            DataAbertura = rd.GetDateTime("data_abertura")
                        };
                    }
                }
            }
        }

        // Equipamentos de UM cliente (alimenta o ComboBox de equipamento)
        public DataTable ListarEquipamentos(int clienteId)
        {
            string sql = @"SELECT id,
                                  CONCAT_WS(' ', tipo, marca, modelo,
                                            CONCAT('(', numero_serie, ')')) AS descricao
                           FROM equipamentos
                           WHERE cliente_id=@c
                           ORDER BY tipo";
            return Consultar(sql, "@c", clienteId);
        }

        public DataTable ListarItens(int osId)
        {
            string sql = @"SELECT i.id, s.descricao AS servico, i.quantidade, i.preco_unitario,
                                  (i.quantidade * i.preco_unitario) AS subtotal
                           FROM itens_os i
                           JOIN servicos s ON s.id = i.servico_id
                           WHERE i.ordem_servico_id=@id
                           ORDER BY i.id";
            return Consultar(sql, "@id", osId);
        }

        public DataTable ListarHistorico(int osId)
        {
            string sql = @"SELECT h.data_alteracao, u.nome AS usuario,
                                  h.status_anterior, h.status_novo, h.justificativa
                           FROM historico_os h
                           JOIN usuarios u ON u.id = h.usuario_id
                           WHERE h.ordem_servico_id=@id
                           ORDER BY h.id";
            return Consultar(sql, "@id", osId);
        }

        // OPERAÇÕES COM TRANSAÇÃO (várias tabelas)

        // Abre a OS e grava o 1º registro do histórico: tudo ou nada.
        public int Abrir(OrdemServico os, int usuarioId)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                using (MySqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        int novoId;
                        string sql = @"INSERT INTO ordens_servico
                                           (cliente_id, equipamento_id, tecnico_id, usuario_abertura_id,
                                            prioridade, descricao_problema)
                                       VALUES (@cli, @eqp, @tec, @usr, @prio, @prob)";
                        using (var cmd = new MySqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@cli", os.ClienteId);
                            cmd.Parameters.AddWithValue("@eqp", os.EquipamentoId);
                            cmd.Parameters.AddWithValue("@tec", NuloInt(os.TecnicoId));
                            cmd.Parameters.AddWithValue("@usr", usuarioId);
                            cmd.Parameters.AddWithValue("@prio", os.Prioridade);
                            cmd.Parameters.AddWithValue("@prob", os.DescricaoProblema.Trim());
                            cmd.ExecuteNonQuery();
                            novoId = Convert.ToInt32(cmd.LastInsertedId);
                        }

                        RegistrarHistorico(conn, tx, novoId, usuarioId, null,
                                           FluxoStatus.Aberta, "Abertura da OS");
                        tx.Commit();
                        return novoId;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // Muda o status, grava os dados da tela e registra o histórico: tudo ou nada.
        // 'dados' traz o que está na tela (técnico, prioridade, problema, diagnóstico, solução).
        public void MudarStatus(OrdemServico dados, string novoStatus, int usuarioId, string justificativa)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                using (MySqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        // trava a linha e lê o status que está de verdade no banco
                        string anterior;
                        using (var cmd = new MySqlCommand(
                            "SELECT status FROM ordens_servico WHERE id=@id FOR UPDATE", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", dados.Id);
                            anterior = cmd.ExecuteScalar() as string;
                        }
                        if (anterior == null)
                            throw new InvalidOperationException("Ordem de serviço não encontrada.");

                        if (!FluxoStatus.TransicaoValida(anterior, novoStatus))
                            throw new InvalidOperationException("Mudança de status não permitida: de \"" +
                                FluxoStatus.Descricao(anterior) + "\" para \"" +
                                FluxoStatus.Descricao(novoStatus) + "\".");

                        bool cancelando = novoStatus == FluxoStatus.Cancelada;
                        if (cancelando && string.IsNullOrWhiteSpace(justificativa))
                            throw new InvalidOperationException("Informe a justificativa do cancelamento.");

                        ValidarRegras(conn, tx, dados, novoStatus);

                        string sql = @"UPDATE ordens_servico
                                       SET status=@novo, tecnico_id=@tec, prioridade=@prio,
                                           descricao_problema=@prob, diagnostico=@diag, solucao=@sol,
                                           justificativa_cancelamento=@just
                                       WHERE id=@id AND status=@ant";
                        int linhas;
                        using (var cmd = new MySqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@novo", novoStatus);
                            cmd.Parameters.AddWithValue("@tec", NuloInt(dados.TecnicoId));
                            cmd.Parameters.AddWithValue("@prio", dados.Prioridade);
                            cmd.Parameters.AddWithValue("@prob", dados.DescricaoProblema.Trim());
                            cmd.Parameters.AddWithValue("@diag", Nulo(dados.Diagnostico));
                            cmd.Parameters.AddWithValue("@sol", Nulo(dados.Solucao));
                            cmd.Parameters.AddWithValue("@just", cancelando ? Nulo(justificativa) : DBNull.Value);
                            cmd.Parameters.AddWithValue("@id", dados.Id);
                            cmd.Parameters.AddWithValue("@ant", anterior);
                            linhas = cmd.ExecuteNonQuery();
                        }
                        if (linhas == 0)
                            throw new InvalidOperationException(
                                "A OS foi alterada por outro usuário. Selecione-a novamente e tente de novo.");

                        RegistrarHistorico(conn, tx, dados.Id, usuarioId, anterior, novoStatus,
                                           cancelando ? justificativa.Trim() : justificativa);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // OPERAÇÕES SIMPLES

        // Salva os dados editáveis sem mudar o status (só se a OS não estiver finalizada)
        public void AtualizarDados(OrdemServico os)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"UPDATE ordens_servico
                               SET tecnico_id=@tec, prioridade=@prio, descricao_problema=@prob,
                                   diagnostico=@diag, solucao=@sol
                               WHERE id=@id AND status NOT IN ('CONCLUIDA','CANCELADA')";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tec", NuloInt(os.TecnicoId));
                    cmd.Parameters.AddWithValue("@prio", os.Prioridade);
                    cmd.Parameters.AddWithValue("@prob", os.DescricaoProblema.Trim());
                    cmd.Parameters.AddWithValue("@diag", Nulo(os.Diagnostico));
                    cmd.Parameters.AddWithValue("@sol", Nulo(os.Solucao));
                    cmd.Parameters.AddWithValue("@id", os.Id);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException(
                            "Esta OS está finalizada (ou não existe) e não pode ser alterada.");
                }
            }
        }

        // O preço vem do cadastro de serviços e fica gravado no item ("foto" do preço).
        // Se o serviço já está na OS, soma a quantidade. O subtotal/total
        // são recalculados pelas triggers do banco.
        public void AdicionarItem(int osId, int servicoId, decimal quantidade)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"INSERT INTO itens_os (ordem_servico_id, servico_id, quantidade, preco_unitario)
                               SELECT @os, s.id, @qtd, s.preco
                               FROM servicos s
                               WHERE s.id=@srv AND s.ativo=1
                               ON DUPLICATE KEY UPDATE quantidade = quantidade + VALUES(quantidade)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@os", osId);
                    cmd.Parameters.AddWithValue("@qtd", quantidade);
                    cmd.Parameters.AddWithValue("@srv", servicoId);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("Serviço indisponível (inativo ou inexistente).");
                }
            }
        }

        public void RemoverItem(int itemId)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                using (var cmd = new MySqlCommand("DELETE FROM itens_os WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", itemId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Desconto nunca pode passar do subtotal. O total é recalculado pela trigger.
        public void DefinirDesconto(int osId, decimal desconto)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = @"UPDATE ordens_servico SET desconto=@d
                               WHERE id=@id AND subtotal >= @d
                                 AND status NOT IN ('CONCLUIDA','CANCELADA')";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@d", desconto);
                    cmd.Parameters.AddWithValue("@id", osId);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException(
                            "O desconto não pode ser maior que o subtotal, e a OS não pode estar finalizada.");
                }
            }
        }

        // AUXILIARES

        private void ValidarRegras(MySqlConnection conn, MySqlTransaction tx, OrdemServico os, string novoStatus)
        {
            int itens;
            decimal total;

            using (var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM itens_os WHERE ordem_servico_id=@id", conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", os.Id);
                itens = Convert.ToInt32(cmd.ExecuteScalar());
            }
            using (var cmd = new MySqlCommand(
                "SELECT total FROM ordens_servico WHERE id=@id", conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", os.Id);
                total = Convert.ToDecimal(cmd.ExecuteScalar());
            }

            bool semDiagnostico = string.IsNullOrWhiteSpace(os.Diagnostico);

            if (novoStatus == FluxoStatus.Diagnostico && !os.TecnicoId.HasValue)
                throw new InvalidOperationException("Atribua um técnico antes de iniciar o diagnóstico.");

            if (novoStatus == FluxoStatus.AguardandoAprovacao)
            {
                if (semDiagnostico)
                    throw new InvalidOperationException("Registre o diagnóstico antes de enviar para aprovação.");
                if (itens == 0)
                    throw new InvalidOperationException("Inclua ao menos um serviço no orçamento antes de enviar para aprovação.");
            }

            if (novoStatus == FluxoStatus.Concluida)
            {
                if (semDiagnostico)
                    throw new InvalidOperationException("Não é possível concluir a OS sem diagnóstico.");
                if (itens == 0)
                    throw new InvalidOperationException("Não é possível concluir a OS sem serviço registrado.");
                if (total <= 0)
                    throw new InvalidOperationException("Não é possível concluir a OS sem valor calculado.");
            }
        }

        private void RegistrarHistorico(MySqlConnection conn, MySqlTransaction tx, int osId,
                                        int usuarioId, string anterior, string novo, string justificativa)
        {
            string sql = @"INSERT INTO historico_os
                               (ordem_servico_id, usuario_id, status_anterior, status_novo, justificativa)
                           VALUES (@os, @usr, @ant, @novo, @just)";
            using (var cmd = new MySqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@os", osId);
                cmd.Parameters.AddWithValue("@usr", usuarioId);
                cmd.Parameters.AddWithValue("@ant", Nulo(anterior));
                cmd.Parameters.AddWithValue("@novo", novo);
                cmd.Parameters.AddWithValue("@just", Nulo(justificativa));
                cmd.ExecuteNonQuery();
            }
        }

        private DataTable Consultar(string sql, string parametro, object valor)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                var da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue(parametro, valor);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private static object Nulo(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor.Trim();
        }

        private static object NuloInt(int? valor)
        {
            return valor.HasValue ? (object)valor.Value : DBNull.Value;
        }
    }
}