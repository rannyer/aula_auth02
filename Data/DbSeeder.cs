using AulaAuth02.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AulaAuth02.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope =  serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<Usuario>>();

            await db.Database.EnsureCreatedAsync();

            if (await db.Usuarios.AnyAsync(u => u.Perfil == Perfis.Admin))
                return;

            var admin = new Usuario
            {
                Nome = "Administrador",
                Email = "admin@aula.com",
                Perfil = Perfis.Admin,
            };
            admin.SenhaHash = hasher.HashPassword(admin, "Admin@13");

            db.Usuarios.Add(admin);
            await db.SaveChangesAsync();



        }
    }
}
