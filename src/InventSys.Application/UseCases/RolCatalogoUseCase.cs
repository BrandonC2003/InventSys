using InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    class RolCatalogoUseCase(IRolCatalogoService rolCatalogoService)
    {
        private readonly IRolCatalogoService _rolCatalogoService = rolCatalogoService;


        public Task<List<RolCatalogo>> ObtenerRolesAsync()
        {
            return _rolCatalogoService.ObtenerRolesAsync();
        }
    }
}
