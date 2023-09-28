using Microsoft.EntityFrameworkCore;
using KafkaConsumerApp.Models;

namespace kafkaConsumerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Usuario { get; set; }
        public DbSet<Rece> Receta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.idusuario);//configura clave primaria de usuario
            modelBuilder.Entity<Rece>().HasKey(r => r.idreceta);//configura clave primaria de usuario
        }
    }
}

/*el código define la clase ApplicationDbContext, que actúa como un contexto de base de datos para interactuar con la base de datos MySQL. 
Define una propiedad Usuario que representa la tabla "Usuario" en la base de datos y configura la clave primaria de esta entidad. 
Entity Framework Core utiliza esta configuración para mapear objetos C# a registros de base de datos y facilitar las operaciones de lectura y escritura en la base de datos.*/