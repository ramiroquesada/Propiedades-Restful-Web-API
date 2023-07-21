using PropiedadesMagicas_API.Datos;
using PropiedadesMagicas_API.Models;
using PropiedadesMagicas_API.Repositorio.IRepositorio;

namespace PropiedadesMagicas_API.Repositorio
{
    public class NumeroPropiedadRepositorio : Repositorio<NumeroPropiedad>, INumeroPropiedadRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroPropiedadRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroPropiedad> Actualizar(NumeroPropiedad entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroPropiedades.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}