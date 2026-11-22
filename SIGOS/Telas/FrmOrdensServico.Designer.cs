namespace SIGOS.Telas
{
    partial class FrmOrdensServico
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPesquisar = new System.Windows.Forms.Label();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.dgvOrdens = new System.Windows.Forms.DataGridView();
            this.colNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEquipamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTecnico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrioridade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAbertura = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgvOrdens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colNumero,
                this.colCliente,
                this.colEquipamento,
                this.colTecnico,
                this.colStatus,
                this.colPrioridade,
                this.colTotal,
                this.colAbertura
            });

            this.colNumero.DataPropertyName = "numero_os";
            this.colNumero.HeaderText = "Nº OS";
            this.colNumero.Name = "colNumero";
            this.colNumero.ReadOnly = true;

            this.colCliente.DataPropertyName = "cliente";
            this.colCliente.HeaderText = "Cliente";
            this.colCliente.Name = "colCliente";
            this.colCliente.ReadOnly = true;

            this.colEquipamento.DataPropertyName = "equipamento";
            this.colEquipamento.HeaderText = "Equipamento";
            this.colEquipamento.Name = "colEquipamento";
            this.colEquipamento.ReadOnly = true;

            this.colTecnico.DataPropertyName = "tecnico";
            this.colTecnico.HeaderText = "Técnico";
            this.colTecnico.Name = "colTecnico";
            this.colTecnico.ReadOnly = true;

            this.colStatus.DataPropertyName = "status";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;

            this.colPrioridade.DataPropertyName = "prioridade";
            this.colPrioridade.HeaderText = "Prioridade";
            this.colPrioridade.Name = "colPrioridade";
            this.colPrioridade.ReadOnly = true;

            this.colTotal.DataPropertyName = "total";
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;

            this.colAbertura.DataPropertyName = "data_abertura";
            this.colAbertura.HeaderText = "Abertura";
            this.colAbertura.Name = "colAbertura";

            this.colAbertura.ReadOnly = true;
            this.tabOS = new System.Windows.Forms.TabControl();
            this.tabDados = new System.Windows.Forms.TabPage();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblDataAbertura = new System.Windows.Forms.Label();
            this.txtDataAbertura = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblPrioridade = new System.Windows.Forms.Label();
            this.cbPrioridade = new System.Windows.Forms.ComboBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.lblEquipamento = new System.Windows.Forms.Label();
            this.cbEquipamento = new System.Windows.Forms.ComboBox();
            this.lblTecnico = new System.Windows.Forms.Label();
            this.cbTecnico = new System.Windows.Forms.ComboBox();
            this.lblProblema = new System.Windows.Forms.Label();
            this.txtProblema = new System.Windows.Forms.TextBox();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.lblSolucao = new System.Windows.Forms.Label();
            this.txtSolucao = new System.Windows.Forms.TextBox();
            this.tabItens = new System.Windows.Forms.TabPage();
            this.lblServico = new System.Windows.Forms.Label();
            this.cbServico = new System.Windows.Forms.ComboBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.nudQuantidade = new System.Windows.Forms.NumericUpDown();
            this.lblPrecoUnit = new System.Windows.Forms.Label();
            this.txtPrecoUnit = new System.Windows.Forms.TextBox();
            this.btnAdicionarItem = new System.Windows.Forms.Button();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.colItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemServico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemPreco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgvItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colItemId,
                this.colItemServico,
                this.colItemQuantidade,
                this.colItemPreco,
                this.colItemSubtotal
            });

            this.colItemId.DataPropertyName = "id";
            this.colItemId.HeaderText = "#";
            this.colItemId.Name = "colItemId";
            this.colItemId.ReadOnly = true;

            this.colItemServico.DataPropertyName = "servico";
            this.colItemServico.HeaderText = "Serviço";
            this.colItemServico.Name = "colItemServico";
            this.colItemServico.ReadOnly = true;

            this.colItemQuantidade.DataPropertyName = "quantidade";
            this.colItemQuantidade.HeaderText = "Qtd";
            this.colItemQuantidade.Name = "colItemQuantidade";
            this.colItemQuantidade.ReadOnly = true;

            this.colItemPreco.DataPropertyName = "preco_unitario";
            this.colItemPreco.HeaderText = "Preço unit.";
            this.colItemPreco.Name = "colItemPreco";
            this.colItemPreco.ReadOnly = true;

            this.colItemSubtotal.DataPropertyName = "subtotal";
            this.colItemSubtotal.HeaderText = "Subtotal";
            this.colItemSubtotal.Name = "colItemSubtotal";

            this.colItemSubtotal.ReadOnly = true;
            this.btnRemoverItem = new System.Windows.Forms.Button();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.btnAplicarDesconto = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.tabHistorico = new System.Windows.Forms.TabPage();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.colHistData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistDe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistPara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistJustificativa = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgvHistorico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colHistData,
                this.colHistUsuario,
                this.colHistDe,
                this.colHistPara,
                this.colHistJustificativa
            });

            this.colHistData.DataPropertyName = "data_alteracao";
            this.colHistData.HeaderText = "Data";
            this.colHistData.Name = "colHistData";
            this.colHistData.ReadOnly = true;

            this.colHistUsuario.DataPropertyName = "usuario";
            this.colHistUsuario.HeaderText = "Usuário";
            this.colHistUsuario.Name = "colHistUsuario";
            this.colHistUsuario.ReadOnly = true;

            this.colHistDe.DataPropertyName = "status_anterior";
            this.colHistDe.HeaderText = "De";
            this.colHistDe.Name = "colHistDe";
            this.colHistDe.ReadOnly = true;

            this.colHistPara.DataPropertyName = "status_novo";
            this.colHistPara.HeaderText = "Para";
            this.colHistPara.Name = "colHistPara";
            this.colHistPara.ReadOnly = true;

            this.colHistJustificativa.DataPropertyName = "justificativa";
            this.colHistJustificativa.HeaderText = "Justificativa";
            this.colHistJustificativa.Name = "colHistJustificativa";

            this.colHistJustificativa.ReadOnly = true;
            this.lblJustificativa = new System.Windows.Forms.Label();
            this.txtJustificativa = new System.Windows.Forms.TextBox();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnAvancar = new System.Windows.Forms.Button();
            this.btnCancelarOS = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btVoltar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdens)).BeginInit();
            this.tabOS.SuspendLayout();
            this.tabDados.SuspendLayout();
            this.tabItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.tabHistorico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Mongolian Baiti", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(207, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Ordens de Serviço";
            // 
            // lblPesquisar
            // 
            this.lblPesquisar.AutoSize = true;
            this.lblPesquisar.Location = new System.Drawing.Point(20, 55);
            this.lblPesquisar.Name = "lblPesquisar";
            this.lblPesquisar.Size = new System.Drawing.Size(56, 13);
            this.lblPesquisar.TabIndex = 1;
            this.lblPesquisar.Text = "Pesquisar:";
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(90, 52);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(280, 20);
            this.txtPesquisa.TabIndex = 2;
            // 
            // dgvOrdens
            // 
            this.dgvOrdens.AllowUserToAddRows = false;
            this.dgvOrdens.AllowUserToDeleteRows = false;
            this.dgvOrdens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdens.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvOrdens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdens.Location = new System.Drawing.Point(20, 82);
            this.dgvOrdens.MultiSelect = false;
            this.dgvOrdens.Name = "dgvOrdens";
            this.dgvOrdens.ReadOnly = true;
            this.dgvOrdens.RowHeadersVisible = false;
            this.dgvOrdens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdens.Size = new System.Drawing.Size(960, 170);
            this.dgvOrdens.TabIndex = 3;
            // 
            // tabOS
            // 
            this.tabOS.Controls.Add(this.tabDados);
            this.tabOS.Controls.Add(this.tabItens);
            this.tabOS.Controls.Add(this.tabHistorico);
            this.tabOS.Location = new System.Drawing.Point(20, 260);
            this.tabOS.Name = "tabOS";
            this.tabOS.SelectedIndex = 0;
            this.tabOS.Size = new System.Drawing.Size(960, 355);
            this.tabOS.TabIndex = 4;
            // 
            // tabDados
            // 
            this.tabDados.Controls.Add(this.lblNumero);
            this.tabDados.Controls.Add(this.txtNumero);
            this.tabDados.Controls.Add(this.lblDataAbertura);
            this.tabDados.Controls.Add(this.txtDataAbertura);
            this.tabDados.Controls.Add(this.lblStatus);
            this.tabDados.Controls.Add(this.txtStatus);
            this.tabDados.Controls.Add(this.lblPrioridade);
            this.tabDados.Controls.Add(this.cbPrioridade);
            this.tabDados.Controls.Add(this.lblCliente);
            this.tabDados.Controls.Add(this.cbCliente);
            this.tabDados.Controls.Add(this.lblEquipamento);
            this.tabDados.Controls.Add(this.cbEquipamento);
            this.tabDados.Controls.Add(this.lblTecnico);
            this.tabDados.Controls.Add(this.cbTecnico);
            this.tabDados.Controls.Add(this.lblProblema);
            this.tabDados.Controls.Add(this.txtProblema);
            this.tabDados.Controls.Add(this.lblDiagnostico);
            this.tabDados.Controls.Add(this.txtDiagnostico);
            this.tabDados.Controls.Add(this.lblSolucao);
            this.tabDados.Controls.Add(this.txtSolucao);
            this.tabDados.Location = new System.Drawing.Point(4, 22);
            this.tabDados.Name = "tabDados";
            this.tabDados.Padding = new System.Windows.Forms.Padding(3);
            this.tabDados.Size = new System.Drawing.Size(952, 329);
            this.tabDados.TabIndex = 0;
            this.tabDados.Text = "Dados da OS";
            this.tabDados.UseVisualStyleBackColor = true;
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(15, 18);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(40, 13);
            this.lblNumero.TabIndex = 0;
            this.lblNumero.Text = "Nº OS:";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(110, 15);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.ReadOnly = true;
            this.txtNumero.Size = new System.Drawing.Size(70, 20);
            this.txtNumero.TabIndex = 1;
            // 
            // lblDataAbertura
            // 
            this.lblDataAbertura.AutoSize = true;
            this.lblDataAbertura.Location = new System.Drawing.Point(205, 18);
            this.lblDataAbertura.Name = "lblDataAbertura";
            this.lblDataAbertura.Size = new System.Drawing.Size(50, 13);
            this.lblDataAbertura.TabIndex = 2;
            this.lblDataAbertura.Text = "Abertura:";
            // 
            // txtDataAbertura
            // 
            this.txtDataAbertura.Location = new System.Drawing.Point(262, 15);
            this.txtDataAbertura.Name = "txtDataAbertura";
            this.txtDataAbertura.ReadOnly = true;
            this.txtDataAbertura.Size = new System.Drawing.Size(120, 20);
            this.txtDataAbertura.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(405, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(451, 15);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(170, 20);
            this.txtStatus.TabIndex = 5;
            // 
            // lblPrioridade
            // 
            this.lblPrioridade.AutoSize = true;
            this.lblPrioridade.Location = new System.Drawing.Point(645, 18);
            this.lblPrioridade.Name = "lblPrioridade";
            this.lblPrioridade.Size = new System.Drawing.Size(57, 13);
            this.lblPrioridade.TabIndex = 6;
            this.lblPrioridade.Text = "Prioridade:";
            // 
            // cbPrioridade
            // 
            this.cbPrioridade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrioridade.FormattingEnabled = true;
            this.cbPrioridade.Items.AddRange(new object[] {
            "BAIXA",
            "MEDIA",
            "ALTA",
            "URGENTE"});
            this.cbPrioridade.Location = new System.Drawing.Point(709, 15);
            this.cbPrioridade.Name = "cbPrioridade";
            this.cbPrioridade.Size = new System.Drawing.Size(130, 21);
            this.cbPrioridade.TabIndex = 7;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(15, 55);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(42, 13);
            this.lblCliente.TabIndex = 8;
            this.lblCliente.Text = "Cliente:";
            // 
            // cbCliente
            // 
            this.cbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(110, 52);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(272, 21);
            this.cbCliente.TabIndex = 9;
            // 
            // lblEquipamento
            // 
            this.lblEquipamento.AutoSize = true;
            this.lblEquipamento.Location = new System.Drawing.Point(405, 55);
            this.lblEquipamento.Name = "lblEquipamento";
            this.lblEquipamento.Size = new System.Drawing.Size(72, 13);
            this.lblEquipamento.TabIndex = 10;
            this.lblEquipamento.Text = "Equipamento:";
            // 
            // cbEquipamento
            // 
            this.cbEquipamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquipamento.FormattingEnabled = true;
            this.cbEquipamento.Location = new System.Drawing.Point(485, 52);
            this.cbEquipamento.Name = "cbEquipamento";
            this.cbEquipamento.Size = new System.Drawing.Size(354, 21);
            this.cbEquipamento.TabIndex = 11;
            // 
            // lblTecnico
            // 
            this.lblTecnico.AutoSize = true;
            this.lblTecnico.Location = new System.Drawing.Point(15, 92);
            this.lblTecnico.Name = "lblTecnico";
            this.lblTecnico.Size = new System.Drawing.Size(49, 13);
            this.lblTecnico.TabIndex = 12;
            this.lblTecnico.Text = "Técnico:";
            // 
            // cbTecnico
            // 
            this.cbTecnico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTecnico.FormattingEnabled = true;
            this.cbTecnico.Location = new System.Drawing.Point(110, 89);
            this.cbTecnico.Name = "cbTecnico";
            this.cbTecnico.Size = new System.Drawing.Size(272, 21);
            this.cbTecnico.TabIndex = 13;
            // 
            // lblProblema
            // 
            this.lblProblema.AutoSize = true;
            this.lblProblema.Location = new System.Drawing.Point(15, 130);
            this.lblProblema.Name = "lblProblema";
            this.lblProblema.Size = new System.Drawing.Size(94, 13);
            this.lblProblema.TabIndex = 14;
            this.lblProblema.Text = "Problema (cliente):";
            // 
            // txtProblema
            // 
            this.txtProblema.Location = new System.Drawing.Point(110, 127);
            this.txtProblema.Multiline = true;
            this.txtProblema.Name = "txtProblema";
            this.txtProblema.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProblema.Size = new System.Drawing.Size(729, 55);
            this.txtProblema.TabIndex = 15;
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.AutoSize = true;
            this.lblDiagnostico.Location = new System.Drawing.Point(15, 200);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(66, 13);
            this.lblDiagnostico.TabIndex = 16;
            this.lblDiagnostico.Text = "Diagnóstico:";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.Location = new System.Drawing.Point(110, 197);
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiagnostico.Size = new System.Drawing.Size(729, 55);
            this.txtDiagnostico.TabIndex = 17;
            // 
            // lblSolucao
            // 
            this.lblSolucao.AutoSize = true;
            this.lblSolucao.Location = new System.Drawing.Point(15, 270);
            this.lblSolucao.Name = "lblSolucao";
            this.lblSolucao.Size = new System.Drawing.Size(92, 13);
            this.lblSolucao.TabIndex = 18;
            this.lblSolucao.Text = "Solução aplicada:";
            // 
            // txtSolucao
            // 
            this.txtSolucao.Location = new System.Drawing.Point(110, 267);
            this.txtSolucao.Multiline = true;
            this.txtSolucao.Name = "txtSolucao";
            this.txtSolucao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSolucao.Size = new System.Drawing.Size(729, 55);
            this.txtSolucao.TabIndex = 19;
            // 
            // tabItens
            // 
            this.tabItens.Controls.Add(this.lblServico);
            this.tabItens.Controls.Add(this.cbServico);
            this.tabItens.Controls.Add(this.lblQuantidade);
            this.tabItens.Controls.Add(this.nudQuantidade);
            this.tabItens.Controls.Add(this.lblPrecoUnit);
            this.tabItens.Controls.Add(this.txtPrecoUnit);
            this.tabItens.Controls.Add(this.btnAdicionarItem);
            this.tabItens.Controls.Add(this.dgvItens);
            this.tabItens.Controls.Add(this.btnRemoverItem);
            this.tabItens.Controls.Add(this.lblSubtotal);
            this.tabItens.Controls.Add(this.txtSubtotal);
            this.tabItens.Controls.Add(this.lblDesconto);
            this.tabItens.Controls.Add(this.txtDesconto);
            this.tabItens.Controls.Add(this.btnAplicarDesconto);
            this.tabItens.Controls.Add(this.lblTotal);
            this.tabItens.Controls.Add(this.txtTotal);
            this.tabItens.Location = new System.Drawing.Point(4, 22);
            this.tabItens.Name = "tabItens";
            this.tabItens.Padding = new System.Windows.Forms.Padding(3);
            this.tabItens.Size = new System.Drawing.Size(952, 329);
            this.tabItens.TabIndex = 1;
            this.tabItens.Text = "Serviços e valores";
            this.tabItens.UseVisualStyleBackColor = true;
            // 
            // lblServico
            // 
            this.lblServico.AutoSize = true;
            this.lblServico.Location = new System.Drawing.Point(15, 18);
            this.lblServico.Name = "lblServico";
            this.lblServico.Size = new System.Drawing.Size(46, 13);
            this.lblServico.TabIndex = 0;
            this.lblServico.Text = "Serviço:";
            // 
            // cbServico
            // 
            this.cbServico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServico.FormattingEnabled = true;
            this.cbServico.Location = new System.Drawing.Point(70, 15);
            this.cbServico.Name = "cbServico";
            this.cbServico.Size = new System.Drawing.Size(310, 21);
            this.cbServico.TabIndex = 1;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Location = new System.Drawing.Point(400, 18);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(27, 13);
            this.lblQuantidade.TabIndex = 2;
            this.lblQuantidade.Text = "Qtd:";
            // 
            // nudQuantidade
            // 
            this.nudQuantidade.DecimalPlaces = 2;
            this.nudQuantidade.Location = new System.Drawing.Point(433, 15);
            this.nudQuantidade.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudQuantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudQuantidade.Name = "nudQuantidade";
            this.nudQuantidade.Size = new System.Drawing.Size(75, 20);
            this.nudQuantidade.TabIndex = 3;
            this.nudQuantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPrecoUnit
            // 
            this.lblPrecoUnit.AutoSize = true;
            this.lblPrecoUnit.Location = new System.Drawing.Point(530, 18);
            this.lblPrecoUnit.Name = "lblPrecoUnit";
            this.lblPrecoUnit.Size = new System.Drawing.Size(84, 13);
            this.lblPrecoUnit.TabIndex = 4;
            this.lblPrecoUnit.Text = "Preço unit. (R$):";
            // 
            // txtPrecoUnit
            // 
            this.txtPrecoUnit.Location = new System.Drawing.Point(629, 15);
            this.txtPrecoUnit.Name = "txtPrecoUnit";
            this.txtPrecoUnit.ReadOnly = true;
            this.txtPrecoUnit.Size = new System.Drawing.Size(80, 20);
            this.txtPrecoUnit.TabIndex = 5;
            // 
            // btnAdicionarItem
            // 
            this.btnAdicionarItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAdicionarItem.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarItem.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarItem.Location = new System.Drawing.Point(725, 13);
            this.btnAdicionarItem.Name = "btnAdicionarItem";
            this.btnAdicionarItem.Size = new System.Drawing.Size(114, 24);
            this.btnAdicionarItem.TabIndex = 6;
            this.btnAdicionarItem.Text = "Adicionar item";
            this.btnAdicionarItem.UseVisualStyleBackColor = false;
            // 
            // dgvItens
            // 
            this.dgvItens.AllowUserToAddRows = false;
            this.dgvItens.AllowUserToDeleteRows = false;
            this.dgvItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItens.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItens.Location = new System.Drawing.Point(15, 50);
            this.dgvItens.MultiSelect = false;
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.ReadOnly = true;
            this.dgvItens.RowHeadersVisible = false;
            this.dgvItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItens.Size = new System.Drawing.Size(824, 170);
            this.dgvItens.TabIndex = 7;
            // 
            // btnRemoverItem
            // 
            this.btnRemoverItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnRemoverItem.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoverItem.Location = new System.Drawing.Point(15, 230);
            this.btnRemoverItem.Name = "btnRemoverItem";
            this.btnRemoverItem.Size = new System.Drawing.Size(114, 24);
            this.btnRemoverItem.TabIndex = 8;
            this.btnRemoverItem.Text = "Remover item";
            this.btnRemoverItem.UseVisualStyleBackColor = false;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(440, 236);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(49, 13);
            this.lblSubtotal.TabIndex = 9;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(495, 233);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(90, 20);
            this.txtSubtotal.TabIndex = 10;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Location = new System.Drawing.Point(600, 236);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(79, 13);
            this.lblDesconto.TabIndex = 11;
            this.lblDesconto.Text = "Desconto (R$):";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(686, 233);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(70, 20);
            this.txtDesconto.TabIndex = 12;
            this.txtDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAplicarDesconto
            // 
            this.btnAplicarDesconto.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAplicarDesconto.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicarDesconto.ForeColor = System.Drawing.Color.White;
            this.btnAplicarDesconto.Location = new System.Drawing.Point(765, 231);
            this.btnAplicarDesconto.Name = "btnAplicarDesconto";
            this.btnAplicarDesconto.Size = new System.Drawing.Size(74, 24);
            this.btnAplicarDesconto.TabIndex = 13;
            this.btnAplicarDesconto.Text = "Aplicar";
            this.btnAplicarDesconto.UseVisualStyleBackColor = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(600, 280);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 15);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "Total (R$):";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(686, 277);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(153, 21);
            this.txtTotal.TabIndex = 15;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabHistorico
            // 
            this.tabHistorico.Controls.Add(this.dgvHistorico);
            this.tabHistorico.Location = new System.Drawing.Point(4, 22);
            this.tabHistorico.Name = "tabHistorico";
            this.tabHistorico.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistorico.Size = new System.Drawing.Size(952, 329);
            this.tabHistorico.TabIndex = 2;
            this.tabHistorico.Text = "Histórico de status";
            this.tabHistorico.UseVisualStyleBackColor = true;
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.AllowUserToDeleteRows = false;
            this.dgvHistorico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorico.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorico.Location = new System.Drawing.Point(15, 15);
            this.dgvHistorico.MultiSelect = false;
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.RowHeadersVisible = false;
            this.dgvHistorico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorico.Size = new System.Drawing.Size(824, 290);
            this.dgvHistorico.TabIndex = 0;
            // 
            // lblJustificativa
            // 
            this.lblJustificativa.AutoSize = true;
            this.lblJustificativa.Location = new System.Drawing.Point(20, 626);
            this.lblJustificativa.Name = "lblJustificativa";
            this.lblJustificativa.Size = new System.Drawing.Size(166, 13);
            this.lblJustificativa.TabIndex = 5;
            this.lblJustificativa.Text = "Justificativa (para cancelar a OS):";
            // 
            // txtJustificativa
            // 
            this.txtJustificativa.Location = new System.Drawing.Point(195, 623);
            this.txtJustificativa.MaxLength = 255;
            this.txtJustificativa.Name = "txtJustificativa";
            this.txtJustificativa.Size = new System.Drawing.Size(500, 20);
            this.txtJustificativa.TabIndex = 6;
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(20, 658);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(90, 28);
            this.btnNovo.TabIndex = 7;
            this.btnNovo.Text = "Nova OS";
            this.btnNovo.UseVisualStyleBackColor = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(120, 658);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 28);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "Salvar OS";
            this.btnSalvar.UseVisualStyleBackColor = false;
            // 
            // btnAvancar
            // 
            this.btnAvancar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAvancar.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvancar.ForeColor = System.Drawing.Color.White;
            this.btnAvancar.Location = new System.Drawing.Point(230, 658);
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.Size = new System.Drawing.Size(140, 28);
            this.btnAvancar.TabIndex = 9;
            this.btnAvancar.Text = "Avançar status";
            this.btnAvancar.UseVisualStyleBackColor = false;
            // 
            // btnCancelarOS
            // 
            this.btnCancelarOS.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancelarOS.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarOS.ForeColor = System.Drawing.Color.White;
            this.btnCancelarOS.Location = new System.Drawing.Point(380, 658);
            this.btnCancelarOS.Name = "btnCancelarOS";
            this.btnCancelarOS.Size = new System.Drawing.Size(110, 28);
            this.btnCancelarOS.TabIndex = 10;
            this.btnCancelarOS.Text = "Cancelar OS";
            this.btnCancelarOS.UseVisualStyleBackColor = false;
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.Color.White;
            this.btnLimpar.Location = new System.Drawing.Point(500, 658);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(90, 28);
            this.btnLimpar.TabIndex = 11;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            // 
            // btVoltar
            // 
            this.btVoltar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btVoltar.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btVoltar.ForeColor = System.Drawing.Color.White;
            this.btVoltar.Location = new System.Drawing.Point(890, 658);
            this.btVoltar.Name = "btVoltar";
            this.btVoltar.Size = new System.Drawing.Size(90, 28);
            this.btVoltar.TabIndex = 12;
            this.btVoltar.Text = "Voltar";
            this.btVoltar.UseVisualStyleBackColor = false;
            // 
            // FrmOrdensServico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.btVoltar);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnCancelarOS);
            this.Controls.Add(this.btnAvancar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.txtJustificativa);
            this.Controls.Add(this.lblJustificativa);
            this.Controls.Add(this.tabOS);
            this.Controls.Add(this.dgvOrdens);
            this.Controls.Add(this.txtPesquisa);
            this.Controls.Add(this.lblPesquisar);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FrmOrdensServico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGOS - Ordens de Serviço";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdens)).EndInit();
            this.tabOS.ResumeLayout(false);
            this.tabDados.ResumeLayout(false);
            this.tabDados.PerformLayout();
            this.tabItens.ResumeLayout(false);
            this.tabItens.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.tabHistorico.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPesquisar;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.DataGridView dgvOrdens;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEquipamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTecnico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrioridade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAbertura;
        private System.Windows.Forms.TabControl tabOS;
        private System.Windows.Forms.TabPage tabDados;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblDataAbertura;
        private System.Windows.Forms.TextBox txtDataAbertura;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblPrioridade;
        private System.Windows.Forms.ComboBox cbPrioridade;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Label lblEquipamento;
        private System.Windows.Forms.ComboBox cbEquipamento;
        private System.Windows.Forms.Label lblTecnico;
        private System.Windows.Forms.ComboBox cbTecnico;
        private System.Windows.Forms.Label lblProblema;
        private System.Windows.Forms.TextBox txtProblema;
        private System.Windows.Forms.Label lblDiagnostico;
        private System.Windows.Forms.TextBox txtDiagnostico;
        private System.Windows.Forms.Label lblSolucao;
        private System.Windows.Forms.TextBox txtSolucao;
        private System.Windows.Forms.TabPage tabItens;
        private System.Windows.Forms.Label lblServico;
        private System.Windows.Forms.ComboBox cbServico;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.NumericUpDown nudQuantidade;
        private System.Windows.Forms.Label lblPrecoUnit;
        private System.Windows.Forms.TextBox txtPrecoUnit;
        private System.Windows.Forms.Button btnAdicionarItem;
        private System.Windows.Forms.DataGridView dgvItens;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemServico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemPreco;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemSubtotal;
        private System.Windows.Forms.Button btnRemoverItem;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.Button btnAplicarDesconto;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TabPage tabHistorico;
        private System.Windows.Forms.DataGridView dgvHistorico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistDe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistPara;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistJustificativa;
        private System.Windows.Forms.Label lblJustificativa;
        private System.Windows.Forms.TextBox txtJustificativa;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnAvancar;
        private System.Windows.Forms.Button btnCancelarOS;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btVoltar;
    }
}