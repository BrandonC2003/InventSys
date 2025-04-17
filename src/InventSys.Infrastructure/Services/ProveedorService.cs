using InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;
using InventSys.Domain.Exceptions;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InventSys.Infrastructure.Services
{
    public class ProveedorService(InventSysDbContext context) : IProveedorService
    {
        private readonly InventSysDbContext _context = context;
        public async Task ActualizarProveedor(int idProveedor, Proveedor proveedor)
        {
            var proveedorExistente = await _context.Proveedores.FindAsync(idProveedor) ??
                throw new KeyNotFoundException("No se encontró un proveedor con ese id");

            proveedorExistente.NombreProveedor = proveedor.NombreProveedor;
            proveedorExistente.CorreoElectronico = proveedor.CorreoElectronico;
            proveedorExistente.Telefono = proveedor.Telefono;

            _context.Proveedores.Update(proveedorExistente);
            await _context.SaveChangesAsync();
        }

        public async Task CrearProveedor(Proveedor proveedor)
        {
            var nuevoProveedor = new Proveedore
            {
                NombreProveedor = proveedor.NombreProveedor,
                CorreoElectronico = proveedor.CorreoElectronico,
                Telefono = proveedor.Telefono
            };

            _context.Proveedores.Add(nuevoProveedor);

            await _context.SaveChangesAsync();
        }

        public async Task EliminarProveedor(int id)
        {
            var proveedorEliminar = await _context.Proveedores.FindAsync(id) ??
                throw new KeyNotFoundException("No se encontró un proveedor con ese id");

            _context.Proveedores.Remove(proveedorEliminar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DeleteException("El Proveedor no se puede eliminar por que hay otros registros que dependen de él");
            }
            
        }

        public async Task<List<Proveedor>> ObtenerProveedores()
        {
            var proveedores = await _context.Proveedores
                .Select(p => new Proveedor
                {
                    IdProveedor = p.IdProveedor,
                    NombreProveedor = p.NombreProveedor,
                    CorreoElectronico = p.CorreoElectronico ?? string.Empty,
                    Telefono = p.Telefono
                }).ToListAsync();

            return proveedores;
        }

        public async Task<Proveedor> ObtenerProveedorPorId(int id)
        {
            var proveedor = await _context.Proveedores
                            .Where(p => p.IdProveedor == id)
                            .Select(p => new Proveedor
                            {
                                IdProveedor = p.IdProveedor,
                                NombreProveedor = p.NombreProveedor,
                                CorreoElectronico = p.CorreoElectronico ?? string.Empty,
                                Telefono = p.Telefono
                            }).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No se encontró un proveedor con ese id");

            return proveedor;
        }

        public async Task<Proveedor> ObtenerProveedorPorNombre(string nombreProveedor)
        {
            var proveedor = await _context.Proveedores
                            .Where(p => p.NombreProveedor == nombreProveedor)
                            .Select(p => new Proveedor
                            {
                                IdProveedor = p.IdProveedor,
                                NombreProveedor = p.NombreProveedor,
                                CorreoElectronico = p.CorreoElectronico ?? string.Empty,
                                Telefono = p.Telefono
                            }).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No se encontró un proveedor con ese nombre");

            return proveedor;
        }
    }
}
