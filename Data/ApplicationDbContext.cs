using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Models;

namespace PropiedadesMinimalApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        public DbSet<Propiedad> Propiedad { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Propiedad>().HasData(
        //        new Propiedad { IdPropiedad = 1, Nombre = "Casa en la torres", Descripcion = "Amplia casa, recien remodelada, de dos planta", Ubicacion = "Guadalajara", Activa = true, FechaCreacion = DateTime.Now.AddDays(-7) },
        //        new Propiedad { IdPropiedad = 2, Nombre = "Departamentos Bimbo", Descripcion = "Torre de departamentos", Ubicacion = "Tonala", Activa = true, FechaCreacion = DateTime.Now.AddDays(-8) },
        //        new Propiedad { IdPropiedad = 3, Nombre = "Terreno baldio uno", Descripcion = "Terreno baldio en zona con alta plusvalia", Ubicacion = "Zapopan", Activa = true, FechaCreacion = DateTime.Now.AddDays(-7) }
        //        );
        //}

    }
}
