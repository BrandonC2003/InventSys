using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface IRolCatalogoService
    {
        Task<List<RolCatalogo>> ObtenerRolesAsync();
    }
}
