namespace SIGOS.Telas
{
    partial class FrmRelatorios
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
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lblCapTotal = new System.Windows.Forms.Label();
            this.lblValTotal = new System.Windows.Forms.Label();
            this.pnlPendentes = new System.Windows.Forms.Panel();
            this.lblCapPendentes = new System.Windows.Forms.Label();
            this.lblValPendentes = new System.Windows.Forms.Label();
            this.pnlEquipamentos = new System.Windows.Forms.Panel();
            this.lblCapEquipamentos = new System.Windows.Forms.Label();
            this.lblValEquipamentos = new System.Windows.Forms.Label();
            this.pnlPrevisto = new System.Windows.Forms.Panel();
            this.lblCapPrevisto = new System.Windows.Forms.Label();
            this.lblValPrevisto = new System.Windows.Forms.Label();
            this.pnlFaturado = new System.Windows.Forms.Panel();
            this.lblCapFaturado = new System.Windows.Forms.Label();
            this.lblValFaturado = new System.Windows.Forms.Label();
            this.lblResumoStatus = new System.Windows.Forms.Label();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.colStStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStQtd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStValor = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgvStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colStStatus,
                this.colStQtd,
                this.colStValor
            });

            this.colStStatus.DataPropertyName = "status";
            this.colStStatus.HeaderText = "Status";
            this.colStStatus.Name = "colStStatus";
            this.colStStatus.ReadOnly = true;

            this.colStQtd.DataPropertyName = "quantidade";
            this.colStQtd.HeaderText = "Quantidade";
            this.colStQtd.Name = "colStQtd";
            this.colStQtd.ReadOnly = true;

            this.colStValor.DataPropertyName = "valor_total";
            this.colStValor.HeaderText = "Valor total";
            this.colStValor.Name = "colStValor";

            this.colStValor.ReadOnly = true;
            this.lblResumoTecnicos = new System.Windows.Forms.Label();
            this.dgvTecnicos = new System.Windows.Forms.DataGridView();
            this.colTcNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTcAndamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTcConcluidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTcFaturado = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgvTecnicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colTcNome,
                this.colTcAndamento,
                this.colTcConcluidas,
                this.colTcFaturado
            });

            this.colTcNome.DataPropertyName = "tecnico";
            this.colTcNome.HeaderText = "Técnico";
            this.colTcNome.Name = "colTcNome";
            this.colTcNome.ReadOnly = true;

            this.colTcAndamento.DataPropertyName = "em_andamento";
            this.colTcAndamento.HeaderText = "Em andamento";
            this.colTcAndamento.Name = "colTcAndamento";
            this.colTcAndamento.ReadOnly = true;

            this.colTcConcluidas.DataPropertyName = "concluidas";
            this.colTcConcluidas.HeaderText = "Concluídas";
            this.colTcConcluidas.Name = "colTcConcluidas";
            this.colTcConcluidas.ReadOnly = true;

            this.colTcFaturado.DataPropertyName = "faturado";
            this.colTcFaturado.HeaderText = "Faturado";
            this.colTcFaturado.Name = "colTcFaturado";

            this.colTcFaturado.ReadOnly = true;
            this.lblPendentes = new System.Windows.Forms.Label();
            this.dgvPendentes = new System.Windows.Forms.DataGridView();
            this.colPdNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdEquipamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdTecnico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdAbertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgvPendentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colPdNumero,
                this.colPdCliente,
                this.colPdEquipamento,
                this.colPdTecnico,
                this.colPdStatus,
                this.colPdAbertura,
                this.colPdTotal
            });

            this.colPdNumero.DataPropertyName = "numero_os";
            this.colPdNumero.HeaderText = "Nº OS";
            this.colPdNumero.Name = "colPdNumero";
            this.colPdNumero.ReadOnly = true;

            this.colPdCliente.DataPropertyName = "cliente";
            this.colPdCliente.HeaderText = "Cliente";
            this.colPdCliente.Name = "colPdCliente";
            this.colPdCliente.ReadOnly = true;

            this.colPdEquipamento.DataPropertyName = "equipamento";
            this.colPdEquipamento.HeaderText = "Equipamento";
            this.colPdEquipamento.Name = "colPdEquipamento";
            this.colPdEquipamento.ReadOnly = true;

            this.colPdTecnico.DataPropertyName = "tecnico";
            this.colPdTecnico.HeaderText = "Técnico";
            this.colPdTecnico.Name = "colPdTecnico";
            this.colPdTecnico.ReadOnly = true;

            this.colPdStatus.DataPropertyName = "status";
            this.colPdStatus.HeaderText = "Status";
            this.colPdStatus.Name = "colPdStatus";
            this.colPdStatus.ReadOnly = true;

            this.colPdAbertura.DataPropertyName = "data_abertura";
            this.colPdAbertura.HeaderText = "Abertura";
            this.colPdAbertura.Name = "colPdAbertura";
            this.colPdAbertura.ReadOnly = true;

            this.colPdTotal.DataPropertyName = "total";
            this.colPdTotal.HeaderText = "Total";
            this.colPdTotal.Name = "colPdTotal";

            this.colPdTotal.ReadOnly = true;
            this.lblNota = new System.Windows.Forms.Label();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.pnlTotal.SuspendLayout();
            this.pnlPendentes.SuspendLayout();
            this.pnlEquipamentos.SuspendLayout();
            this.pnlPrevisto.SuspendLayout();
            this.pnlFaturado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTecnicos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendentes)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(242, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Painel de Indicadores";
            // 
            // pnlTotal
            // 
            this.pnlTotal.BackColor = System.Drawing.Color.White;
            this.pnlTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotal.Controls.Add(this.lblCapTotal);
            this.pnlTotal.Controls.Add(this.lblValTotal);
            this.pnlTotal.Location = new System.Drawing.Point(20, 50);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(184, 72);
            this.pnlTotal.TabIndex = 1;
            // 
            // lblCapTotal
            // 
            this.lblCapTotal.AutoSize = true;
            this.lblCapTotal.ForeColor = System.Drawing.Color.DimGray;
            this.lblCapTotal.Location = new System.Drawing.Point(10, 8);
            this.lblCapTotal.Name = "lblCapTotal";
            this.lblCapTotal.Size = new System.Drawing.Size(83, 13);
            this.lblCapTotal.TabIndex = 0;
            this.lblCapTotal.Text = "OS cadastradas";
            // 
            // lblValTotal
            // 
            this.lblValTotal.AutoSize = true;
            this.lblValTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValTotal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblValTotal.Location = new System.Drawing.Point(10, 32);
            this.lblValTotal.Name = "lblValTotal";
            this.lblValTotal.Size = new System.Drawing.Size(24, 25);
            this.lblValTotal.TabIndex = 1;
            this.lblValTotal.Text = "0";
            // 
            // pnlPendentes
            // 
            this.pnlPendentes.BackColor = System.Drawing.Color.White;
            this.pnlPendentes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPendentes.Controls.Add(this.lblCapPendentes);
            this.pnlPendentes.Controls.Add(this.lblValPendentes);
            this.pnlPendentes.Location = new System.Drawing.Point(214, 50);
            this.pnlPendentes.Name = "pnlPendentes";
            this.pnlPendentes.Size = new System.Drawing.Size(184, 72);
            this.pnlPendentes.TabIndex = 2;
            // 
            // lblCapPendentes
            // 
            this.lblCapPendentes.AutoSize = true;
            this.lblCapPendentes.ForeColor = System.Drawing.Color.DimGray;
            this.lblCapPendentes.Location = new System.Drawing.Point(10, 8);
            this.lblCapPendentes.Name = "lblCapPendentes";
            this.lblCapPendentes.Size = new System.Drawing.Size(101, 13);
            this.lblCapPendentes.TabIndex = 0;
            this.lblCapPendentes.Text = "Serviços pendentes";
            // 
            // lblValPendentes
            // 
            this.lblValPendentes.AutoSize = true;
            this.lblValPendentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValPendentes.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblValPendentes.Location = new System.Drawing.Point(10, 32);
            this.lblValPendentes.Name = "lblValPendentes";
            this.lblValPendentes.Size = new System.Drawing.Size(24, 25);
            this.lblValPendentes.TabIndex = 1;
            this.lblValPendentes.Text = "0";
            // 
            // pnlEquipamentos
            // 
            this.pnlEquipamentos.BackColor = System.Drawing.Color.White;
            this.pnlEquipamentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEquipamentos.Controls.Add(this.lblCapEquipamentos);
            this.pnlEquipamentos.Controls.Add(this.lblValEquipamentos);
            this.pnlEquipamentos.Location = new System.Drawing.Point(408, 50);
            this.pnlEquipamentos.Name = "pnlEquipamentos";
            this.pnlEquipamentos.Size = new System.Drawing.Size(184, 72);
            this.pnlEquipamentos.TabIndex = 3;
            // 
            // lblCapEquipamentos
            // 
            this.lblCapEquipamentos.AutoSize = true;
            this.lblCapEquipamentos.ForeColor = System.Drawing.Color.DimGray;
            this.lblCapEquipamentos.Location = new System.Drawing.Point(10, 8);
            this.lblCapEquipamentos.Name = "lblCapEquipamentos";
            this.lblCapEquipamentos.Size = new System.Drawing.Size(153, 13);
            this.lblCapEquipamentos.TabIndex = 0;
            this.lblCapEquipamentos.Text = "Equipamentos em manutenção";
            // 
            // lblValEquipamentos
            // 
            this.lblValEquipamentos.AutoSize = true;
            this.lblValEquipamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValEquipamentos.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblValEquipamentos.Location = new System.Drawing.Point(10, 32);
            this.lblValEquipamentos.Name = "lblValEquipamentos";
            this.lblValEquipamentos.Size = new System.Drawing.Size(24, 25);
            this.lblValEquipamentos.TabIndex = 1;
            this.lblValEquipamentos.Text = "0";
            // 
            // pnlPrevisto
            // 
            this.pnlPrevisto.BackColor = System.Drawing.Color.White;
            this.pnlPrevisto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPrevisto.Controls.Add(this.lblCapPrevisto);
            this.pnlPrevisto.Controls.Add(this.lblValPrevisto);
            this.pnlPrevisto.Location = new System.Drawing.Point(602, 50);
            this.pnlPrevisto.Name = "pnlPrevisto";
            this.pnlPrevisto.Size = new System.Drawing.Size(184, 72);
            this.pnlPrevisto.TabIndex = 4;
            // 
            // lblCapPrevisto
            // 
            this.lblCapPrevisto.AutoSize = true;
            this.lblCapPrevisto.ForeColor = System.Drawing.Color.DimGray;
            this.lblCapPrevisto.Location = new System.Drawing.Point(10, 8);
            this.lblCapPrevisto.Name = "lblCapPrevisto";
            this.lblCapPrevisto.Size = new System.Drawing.Size(106, 13);
            this.lblCapPrevisto.TabIndex = 0;
            this.lblCapPrevisto.Text = "Faturamento previsto";
            // 
            // lblValPrevisto
            // 
            this.lblValPrevisto.AutoSize = true;
            this.lblValPrevisto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValPrevisto.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblValPrevisto.Location = new System.Drawing.Point(10, 32);
            this.lblValPrevisto.Name = "lblValPrevisto";
            this.lblValPrevisto.Size = new System.Drawing.Size(86, 25);
            this.lblValPrevisto.TabIndex = 1;
            this.lblValPrevisto.Text = "R$ 0,00";
            // 
            // pnlFaturado
            // 
            this.pnlFaturado.BackColor = System.Drawing.Color.White;
            this.pnlFaturado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFaturado.Controls.Add(this.lblCapFaturado);
            this.pnlFaturado.Controls.Add(this.lblValFaturado);
            this.pnlFaturado.Location = new System.Drawing.Point(796, 50);
            this.pnlFaturado.Name = "pnlFaturado";
            this.pnlFaturado.Size = new System.Drawing.Size(184, 72);
            this.pnlFaturado.TabIndex = 5;
            // 
            // lblCapFaturado
            // 
            this.lblCapFaturado.AutoSize = true;
            this.lblCapFaturado.ForeColor = System.Drawing.Color.DimGray;
            this.lblCapFaturado.Location = new System.Drawing.Point(10, 8);
            this.lblCapFaturado.Name = "lblCapFaturado";
            this.lblCapFaturado.Size = new System.Drawing.Size(111, 13);
            this.lblCapFaturado.TabIndex = 0;
            this.lblCapFaturado.Text = "Faturado (concluídas)";
            // 
            // lblValFaturado
            // 
            this.lblValFaturado.AutoSize = true;
            this.lblValFaturado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValFaturado.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblValFaturado.Location = new System.Drawing.Point(10, 32);
            this.lblValFaturado.Name = "lblValFaturado";
            this.lblValFaturado.Size = new System.Drawing.Size(86, 25);
            this.lblValFaturado.TabIndex = 1;
            this.lblValFaturado.Text = "R$ 0,00";
            // 
            // lblResumoStatus
            // 
            this.lblResumoStatus.AutoSize = true;
            this.lblResumoStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumoStatus.Location = new System.Drawing.Point(20, 136);
            this.lblResumoStatus.Name = "lblResumoStatus";
            this.lblResumoStatus.Size = new System.Drawing.Size(127, 15);
            this.lblResumoStatus.TabIndex = 6;
            this.lblResumoStatus.Text = "Resumo por status";
            // 
            // dgvStatus
            // 
            this.dgvStatus.AllowUserToAddRows = false;
            this.dgvStatus.AllowUserToDeleteRows = false;
            this.dgvStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatus.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatus.Location = new System.Drawing.Point(20, 156);
            this.dgvStatus.MultiSelect = false;
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.ReadOnly = true;
            this.dgvStatus.RowHeadersVisible = false;
            this.dgvStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatus.Size = new System.Drawing.Size(470, 150);
            this.dgvStatus.TabIndex = 7;
            // 
            // lblResumoTecnicos
            // 
            this.lblResumoTecnicos.AutoSize = true;
            this.lblResumoTecnicos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumoTecnicos.Location = new System.Drawing.Point(510, 136);
            this.lblResumoTecnicos.Name = "lblResumoTecnicos";
            this.lblResumoTecnicos.Size = new System.Drawing.Size(101, 15);
            this.lblResumoTecnicos.TabIndex = 8;
            this.lblResumoTecnicos.Text = "OS por técnico";
            // 
            // dgvTecnicos
            // 
            this.dgvTecnicos.AllowUserToAddRows = false;
            this.dgvTecnicos.AllowUserToDeleteRows = false;
            this.dgvTecnicos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTecnicos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTecnicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTecnicos.Location = new System.Drawing.Point(510, 156);
            this.dgvTecnicos.MultiSelect = false;
            this.dgvTecnicos.Name = "dgvTecnicos";
            this.dgvTecnicos.ReadOnly = true;
            this.dgvTecnicos.RowHeadersVisible = false;
            this.dgvTecnicos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTecnicos.Size = new System.Drawing.Size(470, 150);
            this.dgvTecnicos.TabIndex = 9;
            // 
            // lblPendentes
            // 
            this.lblPendentes.AutoSize = true;
            this.lblPendentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendentes.Location = new System.Drawing.Point(20, 320);
            this.lblPendentes.Name = "lblPendentes";
            this.lblPendentes.Size = new System.Drawing.Size(346, 15);
            this.lblPendentes.TabIndex = 10;
            this.lblPendentes.Text = "Serviços pendentes e equipamentos em manutenção";
            // 
            // dgvPendentes
            // 
            this.dgvPendentes.AllowUserToAddRows = false;
            this.dgvPendentes.AllowUserToDeleteRows = false;
            this.dgvPendentes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPendentes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPendentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendentes.Location = new System.Drawing.Point(20, 340);
            this.dgvPendentes.MultiSelect = false;
            this.dgvPendentes.Name = "dgvPendentes";
            this.dgvPendentes.ReadOnly = true;
            this.dgvPendentes.RowHeadersVisible = false;
            this.dgvPendentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendentes.Size = new System.Drawing.Size(960, 290);
            this.dgvPendentes.TabIndex = 11;
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.ForeColor = System.Drawing.Color.DimGray;
            this.lblNota.Location = new System.Drawing.Point(130, 653);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(416, 13);
            this.lblNota.TabIndex = 12;
            this.lblNota.Text = "Faturamento previsto = total das OS em andamento (não concluídas nem canceladas)." +
    "";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAtualizar.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(20, 645);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(100, 28);
            this.btnAtualizar.TabIndex = 13;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Location = new System.Drawing.Point(890, 645);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(90, 28);
            this.btnVoltar.TabIndex = 14;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            // 
            // FrmRelatorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1000, 690);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.dgvPendentes);
            this.Controls.Add(this.lblPendentes);
            this.Controls.Add(this.dgvTecnicos);
            this.Controls.Add(this.lblResumoTecnicos);
            this.Controls.Add(this.dgvStatus);
            this.Controls.Add(this.lblResumoStatus);
            this.Controls.Add(this.pnlFaturado);
            this.Controls.Add(this.pnlPrevisto);
            this.Controls.Add(this.pnlEquipamentos);
            this.Controls.Add(this.pnlPendentes);
            this.Controls.Add(this.pnlTotal);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FrmRelatorios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGOS - Painel de Indicadores";
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.pnlPendentes.ResumeLayout(false);
            this.pnlPendentes.PerformLayout();
            this.pnlEquipamentos.ResumeLayout(false);
            this.pnlEquipamentos.PerformLayout();
            this.pnlPrevisto.ResumeLayout(false);
            this.pnlPrevisto.PerformLayout();
            this.pnlFaturado.ResumeLayout(false);
            this.pnlFaturado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTecnicos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendentes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblCapTotal;
        private System.Windows.Forms.Label lblValTotal;
        private System.Windows.Forms.Panel pnlPendentes;
        private System.Windows.Forms.Label lblCapPendentes;
        private System.Windows.Forms.Label lblValPendentes;
        private System.Windows.Forms.Panel pnlEquipamentos;
        private System.Windows.Forms.Label lblCapEquipamentos;
        private System.Windows.Forms.Label lblValEquipamentos;
        private System.Windows.Forms.Panel pnlPrevisto;
        private System.Windows.Forms.Label lblCapPrevisto;
        private System.Windows.Forms.Label lblValPrevisto;
        private System.Windows.Forms.Panel pnlFaturado;
        private System.Windows.Forms.Label lblCapFaturado;
        private System.Windows.Forms.Label lblValFaturado;
        private System.Windows.Forms.Label lblResumoStatus;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStQtd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStValor;
        private System.Windows.Forms.Label lblResumoTecnicos;
        private System.Windows.Forms.DataGridView dgvTecnicos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcAndamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcConcluidas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcFaturado;
        private System.Windows.Forms.Label lblPendentes;
        private System.Windows.Forms.DataGridView dgvPendentes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdEquipamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdTecnico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdAbertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdTotal;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnVoltar;
    }
}