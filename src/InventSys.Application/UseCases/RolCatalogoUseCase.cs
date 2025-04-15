using InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class RolCatalogoUseCase(IRolCatalogoService rolCatalogoService)
    {
        private readonly IRolCatalogoService _rolCatalogoService = rolCatalogoService;


        public Task<List<RolCatalogo>> ObtenerRolesAsync()
        {
            return _rolCatalogoService.ObtenerRolesAsync();
        }

        public Task<RolCatalogo> ObtenerRol(int idRol)
        {
            return _rolCatalogoService.ObtenerRol(idRol);
        }
    }
}
