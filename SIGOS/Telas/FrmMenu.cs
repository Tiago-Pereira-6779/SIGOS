using SIGOS.Classes;
using System;
using System.Windows.Forms;

namespace SIGOS.Telas
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            Text = "SIGOS - Menu Principal  |  " + Sessao.UsuarioLogado.Nome +
                   " (" + Sessao.UsuarioLogado.Perfil + ")";
            AplicarPermissoes();
        }

        // ADMIN:     tudo
        // ATENDENTE: clientes, equipamentos, ordens, consultas e relatórios
        // TECNICO:   ordens e consultas
        private void AplicarPermissoes()
        {
            bool admin = Sessao.TemPerfil("ADMIN");
            bool atendimento = Sessao.TemPerfil("ADMIN", "ATENDENTE");

            btnClientes.Enabled = atendimento;
            btnEquipamentos.Enabled = atendimento;
            btnRelatorios.Enabled = atendimento;
            btnTecnicos.Enabled = admin;
            btnServicos.Enabled = admin;
            // btnOrdensServico e btnConsultas ficam liberados para todos os perfis
        }

        private void Abrir(Form tela)
        {
            using (tela)
            {
                tela.ShowDialog(this);
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Abrir(new FrmClientes());
        }

        private void btnEquipamentos_Click(object sender, EventArgs e)
        {
            Abrir(new FrmEquipamentos());
        }

        private void btnTecnicos_Click(object sender, EventArgs e)
        {
            Abrir(new FrmCadastroTecnico());
        }

        private void btnServicos_Click(object sender, EventArgs e)
        {
            Abrir(new FrmCadastroServicos());
        }

        private void btOrdensServico_Click(object sender, EventArgs e)
        {
            Abrir(new FrmOrdensServico());
        }

        private void btConsultas_Click(object sender, EventArgs e)
        {
            Abrir(new FrmConsultaOrdemServico());
        }

        private void btRelatorios_Click(object sender, EventArgs e)
        {
            Abrir(new FrmRelatorios());
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair do sistema?", "Sair",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Sessao.Encerrar();
                Close();   // fechar o menu encerra o programa
            }
        }
    }
}