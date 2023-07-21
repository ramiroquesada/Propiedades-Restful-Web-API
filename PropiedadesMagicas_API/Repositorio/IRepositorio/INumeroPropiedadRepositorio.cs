using PropiedadesMagicas_API.Models;

namespace PropiedadesMagicas_API.Repositorio.IRepositorio
{
    public interface INumeroPropiedadRepositorio : IRepositorio<NumeroPropiedad>
    {
        Task<NumeroPropiedad> Actualizar(NumeroPropiedad entidad);
    }
}