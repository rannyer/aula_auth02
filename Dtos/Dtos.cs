using AulaAuth02.Entities;
using System.ComponentModel.DataAnnotations;

namespace AulaAuth02.Dtos
{
    public record RegistrarRequest(
        [Required, MaxLength(100)] string Nome,
        [Required, EmailAddress, MaxLength(200)] string Email,
        [Required, MinLength(6)] string Senha
    );
    public record LoginRequest(
        [Required, EmailAddress, MaxLength(200)] string Email,
        [Required, MinLength(6)] string Senha
    );
    public record TokenResponse(string token, DateTime ExpiraEm);
    public record UsuarioResponse(int Id, string nome, string Email, string Perfil)
    {
        public static UsuarioResponse De(Usuario u) => new(u.Id, u.Nome, u.Email, u.Perfil);
    }

}
