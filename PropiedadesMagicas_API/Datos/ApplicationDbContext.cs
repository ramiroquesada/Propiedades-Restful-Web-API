using Microsoft.EntityFrameworkCore;
using PropiedadesMagicas_API.Models;

namespace PropiedadesMagicas_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Propiedad> Propiedades { get; set; }

        public DbSet<NumeroPropiedad> NumeroPropiedades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Propiedad>().HasData(
                new Propiedad()
                {
                    Id = 1,
                    Nombre = "Propiedad Real",
                    Detalles = "Detalle de Propiedad Real...",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 90,
                    Tarifa = 200,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                },
                new Propiedad()
                {
                    Id = 2,
                    Nombre = "Propiedad Barca",
                    Detalles = "Detalle de Propiedad Barca...",
                    ImagenUrl = "",
                    Ocupantes = 3,
                    MetrosCuadrados = 30,
                    Tarifa = 120,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                }

            );
        }
    }
}