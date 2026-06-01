using TrelloBoard.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TrelloBoard.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ========================
            // TABLA USUARIO
            // ========================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Nombre).IsRequired().HasMaxLength(25);
                entity.Property(o => o.Apellidos).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Email).IsRequired().HasMaxLength(100);
                entity.Property(o => o.IdentificadorPokemon).HasDefaultValue(1);
            });

            // ========================
            // TABLA USERSTORY
            // ========================
            modelBuilder.Entity<UserStory>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Titulo).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(o => o.Acceptance_Criteria).IsRequired().HasMaxLength(500);
                entity.Property(o => o.AsignadoA).IsRequired();
                entity.Property(o => o.Estado).IsRequired();
                entity.Property(o => o.Estimacion).IsRequired();
                //relación con Usuario
                entity.HasOne(o => o.Usuario)
                      .WithMany(u => u.UserStories)
                      .HasForeignKey(o => o.AsignadoA)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
