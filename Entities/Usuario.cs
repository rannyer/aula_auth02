namespace AulaAuth02.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string SenhaHash { get; set; } = "";
        public string Perfil { get; set; } = Perfis.Aluno;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
       
    }

    public static class Perfis
    {
        public const string Admin = "Admin";
        public const string Aluno = "Aluno";
    }
    
}
