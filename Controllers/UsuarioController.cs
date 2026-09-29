using AulaAuth02.Entities;
using AulaAuth02.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AulaAuth02.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuarioController(IUsuarioService usuarioService):ControllerBase
    {

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var usuario = await usuarioService.GetByIdAsync(id);
            return usuario is null ? NotFound() : Ok(usuario);
        }
        [HttpGet]
        [Authorize(Roles = Perfis.Admin)]
        public async Task<IActionResult> Listar() => Ok(await usuarioService.GetAllAsync());
        
    }
}
