using Microsoft.EntityFrameworkCore;
using MidiotecaWeb.Models;

namespace MidiotecaWeb.Data
{
    public class MidiotecaDbContext : DbContext
    {
        public MidiotecaDbContext(DbContextOptions<MidiotecaDbContext> options)
            : base(options)
        { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LivroUsuario> LivroUsuarios { get; set; }
        public DbSet<Resenha> Resenhas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

     
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("Id")
                        .HasDefaultValueSql("NEWID()");
                }
            }

          
            modelBuilder.Entity<LivroUsuario>()
                .HasKey(lu => new { lu.LivroId, lu.UsuarioId });

            modelBuilder.Entity<LivroUsuario>()
                .HasOne(lu => lu.Livro)
                .WithMany(l => l.UsuariosRelacionados)
                .HasForeignKey(lu => lu.LivroId);

            modelBuilder.Entity<LivroUsuario>()
                .HasOne(lu => lu.Usuario)
                .WithMany(u => u.LivrosRelacionados)
                .HasForeignKey(lu => lu.UsuarioId);
        }
    }
}
