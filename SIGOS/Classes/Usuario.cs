namespace SIGOS.Classes
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Perfil { get; set; }   // ADMIN, ATENDENTE ou TECNICO
    }
}