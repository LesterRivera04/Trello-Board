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
                entity.Property(o => o.Descripcion).IsRequired().HasMaxLength(50);
                entity.Property(o => o.AsignadoA).IsRequired();
                entity.Property(o => o.Estado).IsRequired();
                entity.Property(o => o.Estimacion).IsRequired();
                //relación con Usuario
                entity.HasOne(o => o.Usuario)
                      .WithMany(u => u.UserStories)
                      .HasForeignKey(o => o.AsignadoA)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            /*
            // ========================
            // DATA INICIAL
            // ========================
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Lester",
                    Apellidos = "Rivera",
                    Email = "les@email.com",
                    IdentificadorPokemon = 1
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Andres",
                    Apellidos = "Dev",
                    Email = "andres@email.com",
                    IdentificadorPokemon = 1
                }
            );

            modelBuilder.Entity<UserStory>().HasData(
                new UserStory
                {
                    Id = 1,
                    Titulo = "Registrar pedido en línea",
                    Descripcion = "Como cliente quiere hacer pedidos",
                    AsignadoA = 1,
                    Estado = UserStoryState.Backlog,
                    Estimacion = 5
                },
                new UserStory
                {
                    Id = 2,
                    Titulo = "Actualizar estado del pedido",
                    Descripcion = "Como cocinero quiere actualizar",
                    AsignadoA = 2,
                    Estado = UserStoryState.InProgress,
                    Estimacion = 8
                }
            );
            */

            //--------------------------------------------------------------------------
            
            /*//fluent API
            modelBuilder.Entity<UserStory>().HasKey(o => o.Id);
            modelBuilder.Entity<UserStory>().Property(o => o.Título).IsRequired().HasMaxLength(50).HasDefaultValue("Titulazo");
            modelBuilder.Entity<UserStory>().Property(o => o.Descripción).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserStory>().Property(o => o.AsignadoA).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserStory>().Property(o => o.Estado).IsRequired();
            modelBuilder.Entity<UserStory>().Property(o => o.Estimación).IsRequired();
            modelBuilder.Entity<UserStory>().HasData(
                new UserStory
                {
                    Id = 1,
                    Título = "Registrar pedido en línea",
                    Descripción = "Como cliente quiero hacer pedidos en línea para ahorrar tiempo",
                    AsignadoA = "María",
                    Estado = UserStoryState.Backlog,
                    Estimación = 5
                },
                new UserStory
                {
                    Id = 2,
                    Título = "Actualizar estado del pedido",
                    Descripción = "Como cocinero quiero cambiar el estado del pedido para notificar avance",
                    AsignadoA = "Andrés",
                    Estado = UserStoryState.InProgress,
                    Estimación = 8
                }
            );*/
        }
    }
}
