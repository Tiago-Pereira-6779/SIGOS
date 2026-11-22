using MySql.Data.MySqlClient;
using SIGOS.BancoDB;
using SIGOS.Classes;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmOrdensServico : Form
    {
        private readonly OrdemServicoDAO osDao = new OrdemServicoDAO();
        private readonly ClienteDAO clienteDao = new ClienteDAO();
        private readonly TecnicoDAO tecnicoDao = new TecnicoDAO();
        private readonly ServicoDAO servicoDao = new ServicoDAO();
        private static readonly CultureInfo ptBR = new CultureInfo("pt-BR");

        private OrdemServico osAtual = null;   // OS aberta na tela (null = nova OS)
        private bool carregando = false;       // evita eventos dos combos durante o preenchimento

        public FrmOrdensServico()
        {
            InitializeComponent();

            // Os eventos são ligados aqui (não pelo designer).
            // Não dê duplo clique nos controles, senão o Visual Studio liga o evento duas vezes.
            Load += FrmOrdensServico_Load;
            txtPesquisa.TextChanged += txtPesquisa_TextChanged;
            dgvOrdens.CellClick += dgvOrdens_CellClick;
            dgvOrdens.CellFormatting += dgvOrdens_CellFormatting;
            dgvHistorico.CellFormatting += dgvHistorico_CellFormatting;
            cbCliente.SelectedIndexChanged += cbCliente_SelectedIndexChanged;
            cbServico.SelectedIndexChanged += cbServico_SelectedIndexChanged;
            btnNovo.Click += btnNovo_Click;
            btnSalvar.Click += btnSalvar_Click;
            btnAvancar.Click += btnAvancar_Click;
            btnCancelarOS.Click += btnCancelarOS_Click;
            btnLimpar.Click += btnLimpar_Click;
            btVoltar.Click += btVoltar_Click;
            btnAdicionarItem.Click += btnAdicionarItem_Click;
            btnRemoverItem.Click += btnRemoverItem_Click;
            btnAplicarDesconto.Click += btnAplicarDesconto_Click;
        }

        //  CARGA INICIAL

        private void FrmOrdensServico_Load(object sender, EventArgs e)
        {
            FormatarGrids();
            CarregarCombos();
            CarregarLista();
            NovaOS();
        }

        private void FormatarGrids()
        {
            DataGridViewContentAlignment direita = DataGridViewContentAlignment.MiddleRight;

            colTotal.DefaultCellStyle.Format = "N2";
            colTotal.DefaultCellStyle.Alignment = direita;
            colAbertura.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            colItemQuantidade.DefaultCellStyle.Format = "N2";
            colItemQuantidade.DefaultCellStyle.Alignment = direita;
            colItemPreco.DefaultCellStyle.Format = "N2";
            colItemPreco.DefaultCellStyle.Alignment = direita;
            colItemSubtotal.DefaultCellStyle.Format = "N2";
            colItemSubtotal.DefaultCellStyle.Alignment = direita;

            colHistData.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void CarregarCombos()
        {
            carregando = true;
            try
            {
                Executar(() =>
                {
                    DataTable clientes = clienteDao.Listar("");
                    cbCliente.DisplayMember = "nome";
                    cbCliente.ValueMember = "id";
                    cbCliente.DataSource = clientes;
                    cbCliente.SelectedIndex = -1;

                    // primeira linha vazia: a OS pode ficar sem técnico
                    DataTable tecnicos = tecnicoDao.Listar("");
                    DataRow vazio = tecnicos.NewRow();
                    vazio["nome"] = "(sem técnico)";
                    tecnicos.Rows.InsertAt(vazio, 0);
                    cbTecnico.DisplayMember = "nome";
                    cbTecnico.ValueMember = "id";
                    cbTecnico.DataSource = tecnicos;
                    cbTecnico.SelectedIndex = 0;

                    DataTable servicos = servicoDao.Listar("");
                    cbServico.DisplayMember = "descricao";
                    cbServico.ValueMember = "id";
                    cbServico.DataSource = servicos;
                    cbServico.SelectedIndex = -1;
                });
            }
            finally
            {
                carregando = false;
            }
        }

        private void CarregarLista()
        {
            Executar(() => dgvOrdens.DataSource = osDao.Listar(txtPesquisa.Text.Trim()));
        }

        private void CarregarEquipamentos(int clienteId)
        {
            Executar(() =>
            {
                DataTable equipamentos = osDao.ListarEquipamentos(clienteId);
                cbEquipamento.DisplayMember = "descricao";
                cbEquipamento.ValueMember = "id";
                cbEquipamento.DataSource = equipamentos;
                cbEquipamento.SelectedIndex = -1;
            });
        }

        //  ESTADO DA TELA (o que pode ser editado em cada situação)

        private void AtualizarEstadoTela()
        {
            bool nova = osAtual == null;
            bool finalizada = !nova && FluxoStatus.Finalizada(osAtual.Status);
            bool abre = Sessao.TemPerfil("ADMIN", "ATENDENTE");
            bool admin = Sessao.TemPerfil("ADMIN");

            bool podeEditarDados = !finalizada && (!nova || abre);
            bool podeItens = !nova && !finalizada;

            btnNovo.Enabled = abre;

            // cliente e equipamento só podem ser escolhidos na abertura
            cbCliente.Enabled = nova && abre;
            cbEquipamento.Enabled = nova && abre;
            cbTecnico.Enabled = podeEditarDados;
            cbPrioridade.Enabled = podeEditarDados;
            txtProblema.ReadOnly = !podeEditarDados;
            txtDiagnostico.ReadOnly = nova || finalizada;
            txtSolucao.ReadOnly = nova || finalizada;

            cbServico.Enabled = podeItens;
            nudQuantidade.Enabled = podeItens;
            btnAdicionarItem.Enabled = podeItens;
            btnRemoverItem.Enabled = podeItens;

            // desconto autorizado: só o administrador
            txtDesconto.ReadOnly = !(podeItens && admin);
            btnAplicarDesconto.Enabled = podeItens && admin;

            btnSalvar.Enabled = podeEditarDados;
            btnAvancar.Enabled = podeItens;
            btnCancelarOS.Enabled = podeItens;
            txtJustificativa.Enabled = podeItens;
        }

        private void NovaOS()
        {
            osAtual = null;
            carregando = true;
            try
            {
                txtNumero.Clear();
                txtDataAbertura.Clear();
                txtStatus.Text = "(nova OS)";
                cbPrioridade.SelectedItem = "MEDIA";
                cbCliente.SelectedIndex = -1;
                cbEquipamento.DataSource = null;
                if (cbTecnico.Items.Count > 0) cbTecnico.SelectedIndex = 0;
                txtProblema.Clear();
                txtDiagnostico.Clear();
                txtSolucao.Clear();
                txtJustificativa.Clear();
                nudQuantidade.Value = 1;
                txtSubtotal.Text = Moeda(0);
                txtDesconto.Text = Moeda(0);
                txtTotal.Text = Moeda(0);
                dgvItens.DataSource = null;
                dgvHistorico.DataSource = null;
                dgvOrdens.ClearSelection();
            }
            finally
            {
                carregando = false;
            }

            tabOS.SelectedTab = tabDados;
            AtualizarEstadoTela();
        }

        // Carrega uma OS existente nos campos da tela
        private void CarregarOS(int id)
        {
            OrdemServico os = null;
            if (!Executar(() => os = osDao.Buscar(id))) return;

            if (os == null)
            {
                MessageBox.Show("Ordem de serviço não encontrada.");
                CarregarLista();
                NovaOS();
                return;
            }

            osAtual = os;
            carregando = true;
            try
            {
                txtNumero.Text = os.Id.ToString();
                txtDataAbertura.Text = os.DataAbertura.ToString("dd/MM/yyyy HH:mm");
                txtStatus.Text = FluxoStatus.Descricao(os.Status);
                cbPrioridade.SelectedItem = os.Prioridade;

                cbCliente.SelectedValue = os.ClienteId;
                CarregarEquipamentos(os.ClienteId);
                cbEquipamento.SelectedValue = os.EquipamentoId;

                if (os.TecnicoId.HasValue)
                    cbTecnico.SelectedValue = os.TecnicoId.Value;
                else
                    cbTecnico.SelectedIndex = 0;

                txtProblema.Text = os.DescricaoProblema;
                txtDiagnostico.Text = os.Diagnostico ?? "";
                txtSolucao.Text = os.Solucao ?? "";
                txtJustificativa.Clear();
                MostrarValores(os);

                Executar(() =>
                {
                    dgvItens.DataSource = osDao.ListarItens(os.Id);
                    dgvHistorico.DataSource = osDao.ListarHistorico(os.Id);
                });
            }
            finally
            {
                carregando = false;
            }

            AtualizarEstadoTela();
        }

        private void MostrarValores(OrdemServico os)
        {
            txtSubtotal.Text = Moeda(os.Subtotal);
            txtDesconto.Text = Moeda(os.Desconto);
            txtTotal.Text = Moeda(os.Total);
        }

        // Depois de mexer em itens/desconto: lê de novo o que o banco calculou
        private void RecarregarItensEValores()
        {
            if (osAtual == null) return;

            int id = osAtual.Id;
            OrdemServico os = null;
            DataTable itens = null;

            if (!Executar(() =>
            {
                os = osDao.Buscar(id);
                itens = osDao.ListarItens(id);
            })) return;

            if (os == null) return;

            osAtual = os;
            MostrarValores(os);
            dgvItens.DataSource = itens;
            CarregarLista();
            SelecionarNaLista(id);
        }

        private void SelecionarNaLista(int id)
        {
            foreach (DataGridViewRow linha in dgvOrdens.Rows)
            {
                if (Convert.ToInt32(linha.Cells[colNumero.Index].Value) == id)
                {
                    linha.Selected = true;
                    dgvOrdens.CurrentCell = linha.Cells[colNumero.Index];
                    break;
                }
            }
        }

        //  EVENTOS DE LISTA / COMBOS

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarLista();
        }

        private void dgvOrdens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int id = Convert.ToInt32(dgvOrdens.Rows[e.RowIndex].Cells[colNumero.Index].Value);
            CarregarOS(id);
        }

        private void dgvOrdens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            if (e.ColumnIndex == colStatus.Index)
            {
                e.Value = FluxoStatus.Descricao(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private void dgvHistorico_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            if (e.ColumnIndex == colHistDe.Index || e.ColumnIndex == colHistPara.Index)
            {
                e.Value = FluxoStatus.Descricao(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        // Escolheu o cliente: o combo de equipamento mostra só os dele
        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregando) return;

            int? clienteId = ComoInt(cbCliente.SelectedValue);
            if (clienteId == null)
            {
                cbEquipamento.DataSource = null;
                return;
            }
            CarregarEquipamentos(clienteId.Value);
        }

        // Escolheu o serviço: mostra o preço do cadastro
        private void cbServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregando) return;

            DataRowView linha = cbServico.SelectedItem as DataRowView;
            txtPrecoUnit.Text = linha == null
                ? ""
                : Convert.ToDecimal(linha["preco"]).ToString("N2", ptBR);
        }

        //  BOTÕES: OS

        private void btnNovo_Click(object sender, EventArgs e)
        {
            NovaOS();
            cbCliente.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisa.Clear();
            NovaOS();
        }

        private void btVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCabecalho()) return;

            if (osAtual == null)
            {
                // abrir nova OS
                if (!Sessao.TemPerfil("ADMIN", "ATENDENTE"))
                {
                    MessageBox.Show("Seu perfil não permite abrir ordens de serviço.");
                    return;
                }

                int? clienteId = ComoInt(cbCliente.SelectedValue);
                int? equipamentoId = ComoInt(cbEquipamento.SelectedValue);

                if (clienteId == null)
                {
                    MessageBox.Show("Selecione o cliente.");
                    cbCliente.Focus();
                    return;
                }
                if (equipamentoId == null)
                {
                    MessageBox.Show("Selecione o equipamento do cliente.");
                    cbEquipamento.Focus();
                    return;
                }

                var nova = new OrdemServico
                {
                    ClienteId = clienteId.Value,
                    EquipamentoId = equipamentoId.Value,
                    TecnicoId = TecnicoSelecionado(),
                    Prioridade = (string)cbPrioridade.SelectedItem,
                    DescricaoProblema = txtProblema.Text
                };

                int novoId = 0;
                if (!Executar(() => novoId = osDao.Abrir(nova, Sessao.UsuarioLogado.Id))) return;

                MessageBox.Show("OS nº " + novoId + " aberta com sucesso!");
                CarregarLista();
                CarregarOS(novoId);
                SelecionarNaLista(novoId);
            }
            else
            {
                // atualizar OS existente
                OrdemServico dados = MontarDados();
                if (!Executar(() => osDao.AtualizarDados(dados))) return;

                MessageBox.Show("OS atualizada com sucesso!");
                CarregarLista();
                CarregarOS(dados.Id);
                SelecionarNaLista(dados.Id);
            }
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            if (osAtual == null) return;
            if (!ValidarCabecalho()) return;

            string atual = osAtual.Status;
            string proximo = FluxoStatus.Proximo(atual);

            if (proximo == null)
            {
                MessageBox.Show("Esta OS não tem um próximo status.");
                return;
            }

            string pergunta = "Avançar a OS nº " + osAtual.Id + " de \"" + FluxoStatus.Descricao(atual) +
                              "\" para \"" + FluxoStatus.Descricao(proximo) + "\"?";
            if (MessageBox.Show(pergunta, "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            OrdemServico dados = MontarDados();
            if (!Executar(() => osDao.MudarStatus(dados, proximo, Sessao.UsuarioLogado.Id, null))) return;

            MessageBox.Show("Status atualizado para \"" + FluxoStatus.Descricao(proximo) + "\".");
            CarregarLista();
            CarregarOS(dados.Id);
            SelecionarNaLista(dados.Id);
        }

        private void btnCancelarOS_Click(object sender, EventArgs e)
        {
            if (osAtual == null) return;

            string justificativa = txtJustificativa.Text.Trim();
            if (justificativa.Length == 0)
            {
                MessageBox.Show("Informe a justificativa do cancelamento no campo abaixo das abas.");
                txtJustificativa.Focus();
                return;
            }
            if (!ValidarCabecalho()) return;

            if (MessageBox.Show("Cancelar a OS nº " + osAtual.Id + "? Esta ação não pode ser desfeita.",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            OrdemServico dados = MontarDados();
            if (!Executar(() => osDao.MudarStatus(dados, FluxoStatus.Cancelada,
                                                  Sessao.UsuarioLogado.Id, justificativa))) return;

            MessageBox.Show("OS cancelada.");
            CarregarLista();
            CarregarOS(dados.Id);
            SelecionarNaLista(dados.Id);
        }

        //  BOTÕES: SERVIÇOS E VALORES

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            if (osAtual == null)
            {
                MessageBox.Show("Salve a OS antes de incluir serviços.");
                return;
            }

            int? servicoId = ComoInt(cbServico.SelectedValue);
            if (servicoId == null)
            {
                MessageBox.Show("Selecione um serviço.");
                cbServico.Focus();
                return;
            }

            decimal quantidade = nudQuantidade.Value;
            if (quantidade <= 0)
            {
                MessageBox.Show("Informe uma quantidade maior que zero.");
                return;
            }

            int osId = osAtual.Id;
            if (!Executar(() => osDao.AdicionarItem(osId, servicoId.Value, quantidade))) return;

            nudQuantidade.Value = 1;
            RecarregarItensEValores();
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (osAtual == null || dgvItens.CurrentRow == null)
            {
                MessageBox.Show("Selecione um item na lista para remover.");
                return;
            }

            int itemId = Convert.ToInt32(dgvItens.CurrentRow.Cells[colItemId.Index].Value);

            if (MessageBox.Show("Remover o item selecionado?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (!Executar(() => osDao.RemoverItem(itemId))) return;
            RecarregarItensEValores();
        }

        private void btnAplicarDesconto_Click(object sender, EventArgs e)
        {
            if (osAtual == null) return;

            if (!Sessao.TemPerfil("ADMIN"))
            {
                MessageBox.Show("Somente o administrador pode autorizar descontos.");
                return;
            }

            decimal desconto = 0;
            string texto = txtDesconto.Text.Replace("R$", "").Trim();

            if (texto.Length > 0 &&
                (!decimal.TryParse(texto, NumberStyles.Number, ptBR, out desconto)
                 || desconto < 0 || desconto > 99999999.99m))
            {
                MessageBox.Show("Informe um desconto válido (ex.: 15,00).");
                txtDesconto.Focus();
                return;
            }
            desconto = Math.Round(desconto, 2);

            int osId = osAtual.Id;
            if (!Executar(() => osDao.DefinirDesconto(osId, desconto))) return;
            RecarregarItensEValores();
        }

        //  AUXILIARES

        private bool ValidarCabecalho()
        {
            if (!(cbPrioridade.SelectedItem is string))
            {
                MessageBox.Show("Selecione a prioridade.");
                tabOS.SelectedTab = tabDados;
                cbPrioridade.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtProblema.Text))
            {
                MessageBox.Show("Descreva o problema relatado pelo cliente.");
                tabOS.SelectedTab = tabDados;
                txtProblema.Focus();
                return false;
            }
            return true;
        }

        // Dados editáveis que estão na tela agora
        private OrdemServico MontarDados()
        {
            return new OrdemServico
            {
                Id = osAtual.Id,
                TecnicoId = TecnicoSelecionado(),
                Prioridade = (string)cbPrioridade.SelectedItem,
                DescricaoProblema = txtProblema.Text,
                Diagnostico = txtDiagnostico.Text,
                Solucao = txtSolucao.Text
            };
        }

        // "(sem técnico)" devolve null. Se o técnico da OS foi inativado e não está
        // mais no combo, mantém o que já estava gravado (não apaga sem querer).
        private int? TecnicoSelecionado()
        {
            if (cbTecnico.SelectedIndex < 0)
                return osAtual != null ? osAtual.TecnicoId : null;

            return ComoInt(cbTecnico.SelectedValue);
        }

        private static int? ComoInt(object valor)
        {
            int n;
            if (valor != null && int.TryParse(Convert.ToString(valor), out n))
                return n;
            return null;
        }

        private static string Moeda(decimal valor)
        {
            return valor.ToString("N2", ptBR);
        }

        // Roda uma operação do banco e mostra mensagens amigáveis.
        // Retorna true se deu certo.
        private bool Executar(Action acao)
        {
            try
            {
                acao();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                // regras de negócio validadas no C#
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException ex) when (ex.Number == 1644)
            {
                // regras barradas pelas triggers do banco
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException ex) when (ex.Number == 1452)
            {
                MessageBox.Show("Cliente ou equipamento inválido: o equipamento precisa pertencer ao cliente.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}