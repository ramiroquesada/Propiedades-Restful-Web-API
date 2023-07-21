using PropiedadesMagicas_API.Datos;
using PropiedadesMagicas_API.Models;
using PropiedadesMagicas_API.Repositorio.IRepositorio;

namespace PropiedadesMagicas_API.Repositorio
{
    public class PropiedadRepositorio : Repositorio<Propiedad>, IPropiedadRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PropiedadRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Propiedad> Actualizar(Propiedad entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Propiedades.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}