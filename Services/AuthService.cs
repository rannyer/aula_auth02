using AulaAuth02.Data;
using AulaAuth02.Dtos;
using AulaAuth02.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AulaAuth02.Services
{
    public interface IAuthService
    {
        Task<UsuarioResponse?> RegistrarAsync(RegistrarRequest req);
        Task<TokenResponse?> LoginAsync(LoginRequest req);
    }
    public class AuthService(
            AppDbContext db,
            IPasswordHasher<Usuario> hasher,
            ITokenService tokenService) : IAuthService
        
    {
        public async Task<TokenResponse?> LoginAsync(LoginRequest req)
        {
            var email = req.Email.Trim().ToLowerInvariant();
            var usuario =  await db.Usuarios.FirstOrDefaultAsync(u =>  u.Email == email);

            if (usuario == null) return null;

            var resultado = hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, req.Senha);

            if(resultado == PasswordVerificationResult.Failed)
                return null;

            if(resultado == PasswordVerificationResult.SuccessRehashNeeded)
            {
                usuario.SenhaHash = hasher.HashPassword(usuario, req.Senha);
                await db.SaveChangesAsync();
            }

            return tokenService.GerarToken(usuario);


        }

        public async Task<UsuarioResponse?> RegistrarAsync(RegistrarRequest req)
        {
            var email = req.Email.Trim().ToLowerInvariant();

            if (await db.Usuarios.AnyAsync(u => u.Email == email))
                return null;
            var usuario = new Usuario()
            {
                Nome = req.Nome.Trim(),
                Email = email,
                Perfil = Perfis.Aluno
            };

            usuario.SenhaHash = hasher.HashPassword(usuario, req.Senha);

            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();

            return UsuarioResponse.De(usuario);
        }

        
    }
}
