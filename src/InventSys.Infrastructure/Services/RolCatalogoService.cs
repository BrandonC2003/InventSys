using DomainEntity = InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InventSys.Infrastructure.Services
{
    public class RolCatalogoService(InventSysDbContext context) : IRolCatalogoService
    {
        private readonly InventSysDbContext _context = context;
        public async Task<List<DomainEntity.RolCatalogo>> ObtenerRolesAsync()
        {
            var roles = await (from r in _context.RolCatalogos
                        select new DomainEntity.RolCatalogo
                        {
                            IdRol = r.IdRol,
                            Rol = r.Rol
                        }).ToListAsync();

            return roles;
        }
    }   
}
