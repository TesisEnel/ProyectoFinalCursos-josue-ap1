using Microsoft.EntityFrameworkCore;
using ProyectoCursos.Shared;

namespace ProyectoCursos.Server.DAL
{
    public class Context : DbContext
    {
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Niveles> Niveles { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuarios>().HasData(new List<Usuarios>()
            {
                new Usuarios(){
                    UsuarioId = 1,
                    NombreCompleto = "Josue Russo",
                    FechaNacimiento = new DateTime(2001, 4, 6),
                    NombreUsuario = "Admin",
                    Email = "Eladmin@gmail.com",
                    Password = "Admin#123",
                    Rol = 1 },
            });

            modelBuilder.Entity<Roles>().HasData(new List<Roles>()
            {
                new Roles(){ RolId = 1, NombreRol = "Administrador" },
                new Roles(){ RolId = 2, NombreRol = "Profesor" },
                new Roles(){ RolId = 3, NombreRol = "Estudiante" },
            });

            modelBuilder.Entity<Niveles>().HasData(new List<Niveles>()
            {
                new Niveles(){ NivelId = 1, Nivelnombre = "Principiante" },
                new Niveles(){ NivelId = 2, Nivelnombre = "Medio" },
                new Niveles(){ NivelId = 3, Nivelnombre = "Avanzado" },
            });
        }
    }
}
