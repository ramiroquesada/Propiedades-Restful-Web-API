using PropiedadesMagicas_API.Models;

namespace PropiedadesMagicas_API.Repositorio.IRepositorio
{
    public interface IPropiedadRepositorio : IRepositorio<Propiedad>
    {
        Task<Propiedad> Actualizar(Propiedad entidad);
    }
}