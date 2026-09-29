using AulaAuth02.Dtos;
using AulaAuth02.Services;
using Microsoft.AspNetCore.Mvc;

namespace AulaAuth02.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarRequest req)
        {
            var usuario = await authService.RegistrarAsync(req);

            return usuario is null
                ? Conflict(new { mensagem = " Email ja cadastrado!" })
                : StatusCode(StatusCodes.Status201Created, usuario);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var token = await authService.LoginAsync(req);

            return token is null 
                ? Unauthorized(new { mensagem = "Email ou senha invalidos"})
                : Ok(token);
        }
    }
}
