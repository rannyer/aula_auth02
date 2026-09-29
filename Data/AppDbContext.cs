using AulaAuth02.Entities;
using Microsoft.EntityFrameworkCore;

namespace AulaAuth02.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) 
        : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(e =>
            {
                e.HasIndex(u => u.Email).IsUnique();
                e.Property(u => u.Nome).HasMaxLength(100).IsRequired();
                e.Property(u => u.Email).HasMaxLength(200).IsRequired();
                e.Property(u => u.SenhaHash).IsRequired();
                e.Property(u => u.Perfil).HasMaxLength(20).IsRequired();
            });
        }
    }
}
