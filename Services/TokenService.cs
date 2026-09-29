using AulaAuth02.Dtos;
using AulaAuth02.Entities;
using AulaAuth02.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AulaAuth02.Services
{
    public interface ITokenService
    {
        TokenResponse GerarToken(Usuario usuario);
    }
    public class TokenService(IOptions<JwtSettings> options) : ITokenService
    {
        private readonly JwtSettings _jwt = options.Value;
        public TokenResponse GerarToken(Usuario usuario)
        {
            var expiraEm = DateTime.UtcNow.AddMinutes(_jwt.ExpiracaoMinutos);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil),
            };

            var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: expiraEm,
                signingCredentials: credenciais
                );

            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
        }

    }
}
