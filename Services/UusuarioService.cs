using AulaAuth02.Data;
using AulaAuth02.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AulaAuth02.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse?> GetByIdAsync(int id);
        Task<List<UsuarioResponse>> GetAllAsync();
    }
    public class UusuarioService(AppDbContext db) : IUsuarioService
    {
        public async Task<List<UsuarioResponse>> GetAllAsync()
        {
            return await db.Usuarios.AsNoTracking()
                  .OrderBy(u => u.Nome)
                  .Select(u => new UsuarioResponse(u.Id, u.Nome, u.Email, u.Perfil))
                  .ToListAsync();
        }

        public async Task<UsuarioResponse?> GetByIdAsync(int id)
        {
            var usuario = await db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id );
            return usuario is null ? null : UsuarioResponse.De(usuario);
        }


    }
}
