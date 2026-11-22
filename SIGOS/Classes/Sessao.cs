using System.Linq;

namespace SIGOS.Classes
{
    // Guarda quem está logado enquanto o sistema está aberto.
    // Qualquer tela pode consultar: Sessao.UsuarioLogado.Nome, Sessao.TemPerfil("ADMIN")...
    public static class Sessao
    {
        public static Usuario UsuarioLogado { get; set; }

        public static bool TemPerfil(params string[] perfis)
        {
            return UsuarioLogado != null && perfis.Contains(UsuarioLogado.Perfil);
        }

        public static void Encerrar()
        {
            UsuarioLogado = null;
        }
    }
}