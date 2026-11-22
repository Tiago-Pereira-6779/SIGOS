namespace SIGOS.Segurança
{
    // Requer o pacote NuGet "BCrypt.Net-Next".
    // A senha NUNCA é guardada em texto puro: só o hash vai para a coluna senha_hash.
    public static class SenhaHelper
    {
        public static string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool Verificar(string senha, string hash)
        {
            if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(hash))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(senha, hash);
            }
            catch (System.Exception)
            {
                return false; // hash inválido/corrompido no banco
            }
        }
    }
}