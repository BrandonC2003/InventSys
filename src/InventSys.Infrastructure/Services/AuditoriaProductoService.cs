using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using EF = InventSys.Infrastructure.Data.EntityFramework;

namespace InventSys.Infrastructure.Services
{
    public class AuditoriaProductoService(EF.InventSysDbContext context) : IAuditoriaProductoService
    {
        private readonly EF.InventSysDbContext _context = context;
        public async Task<List<AuditoriaProducto>> ObtenerAuditoriaPorAccion(AccionAuditoria accion)
        {
            var auditoriasFiltrada = await _context.AuditoriaProductos
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .Where(a => a.Accion == (byte)accion)
                .Select(a => new AuditoriaProducto()
                {
                    IdAuditoriaProducto = a.IdAuditoriaProducto,
                    IdProducto = a.IdProducto,
                    IdUsuario = a.IdUsuario,
                    FechaAccion = a.FechaAccion,
                    Accion = (byte)a.Accion,
                    DatosAnteriores = a.DatosAnteriores,
                    DatosNuevos = a.DatosNuevos,
                    Descripcion = a.Descripcion,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = a.IdProductoNavigation.IdProducto,
                        IdCategoria = a.IdProductoNavigation.IdCategoria,
                        IdProveedor = a.IdProductoNavigation.IdProveedor,
                        NombreProducto = a.IdProductoNavigation.NombreProducto,
                        PrecioCompra = a.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = a.IdProductoNavigation.PrecioVenta,
                        Stok = a.IdProductoNavigation.Stok,
                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = a.IdUsuarioNavigation.IdUsuario,
                        UserName = a.IdUsuarioNavigation.UserName,
                        Nombre = a.IdUsuarioNavigation.Nombre,
                        Apellido = a.IdUsuarioNavigation.Apellido
                    }
                })
                .ToListAsync();

            return auditoriasFiltrada;
        }

        public async Task<List<AuditoriaProducto>> ObtenerAuditoriaPorFecha(DateTime inicio, DateTime fin)
        {
            var auditoriasFiltrada = await _context.AuditoriaProductos
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .Where(a => a.FechaAccion >= inicio && a.FechaAccion <= fin)
                .Select(a => new AuditoriaProducto()
                {
                    IdAuditoriaProducto = a.IdAuditoriaProducto,
                    IdProducto = a.IdProducto,
                    IdUsuario = a.IdUsuario,
                    FechaAccion = a.FechaAccion,
                    Accion = (byte)a.Accion,
                    DatosAnteriores = a.DatosAnteriores,
                    DatosNuevos = a.DatosNuevos,
                    Descripcion = a.Descripcion,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = a.IdProductoNavigation.IdProducto,
                        IdCategoria = a.IdProductoNavigation.IdCategoria,
                        IdProveedor = a.IdProductoNavigation.IdProveedor,
                        NombreProducto = a.IdProductoNavigation.NombreProducto,
                        PrecioCompra = a.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = a.IdProductoNavigation.PrecioVenta,
                        Stok = a.IdProductoNavigation.Stok,
                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = a.IdUsuarioNavigation.IdUsuario,
                        UserName = a.IdUsuarioNavigation.UserName,
                        Nombre = a.IdUsuarioNavigation.Nombre,
                        Apellido = a.IdUsuarioNavigation.Apellido
                    }
                })
                .ToListAsync();

            return auditoriasFiltrada;
        }

        public async Task<List<AuditoriaProducto>> ObtenerAuditoriaPorProducto(int productoId)
        {
            var auditoriasFiltrada = await _context.AuditoriaProductos
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .Where(a => a.IdProducto == productoId)
                .Select(a => new AuditoriaProducto()
                {
                    IdAuditoriaProducto = a.IdAuditoriaProducto,
                    IdProducto = a.IdProducto,
                    IdUsuario = a.IdUsuario,
                    FechaAccion = a.FechaAccion,
                    Accion = (byte)a.Accion,
                    DatosAnteriores = a.DatosAnteriores,
                    DatosNuevos = a.DatosNuevos,
                    Descripcion = a.Descripcion,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = a.IdProductoNavigation.IdProducto,
                        IdCategoria = a.IdProductoNavigation.IdCategoria,
                        IdProveedor = a.IdProductoNavigation.IdProveedor,
                        NombreProducto = a.IdProductoNavigation.NombreProducto,
                        PrecioCompra = a.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = a.IdProductoNavigation.PrecioVenta,
                        Stok = a.IdProductoNavigation.Stok,
                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = a.IdUsuarioNavigation.IdUsuario,
                        UserName = a.IdUsuarioNavigation.UserName,
                        Nombre = a.IdUsuarioNavigation.Nombre,
                        Apellido = a.IdUsuarioNavigation.Apellido
                    }
                })
                .ToListAsync();

            return auditoriasFiltrada;
        }

        public async Task<List<AuditoriaProducto>> ObtenerAuditoriaPorUsuario(int usuarioId)
        {
            var auditoriasFiltrada = await _context.AuditoriaProductos
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .Where(a => a.IdUsuario == usuarioId)
                .Select(a => new AuditoriaProducto()
                {
                    IdAuditoriaProducto = a.IdAuditoriaProducto,
                    IdProducto = a.IdProducto,
                    IdUsuario = a.IdUsuario,
                    FechaAccion = a.FechaAccion,
                    Accion = (byte)a.Accion,
                    DatosAnteriores = a.DatosAnteriores,
                    DatosNuevos = a.DatosNuevos,
                    Descripcion = a.Descripcion,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = a.IdProductoNavigation.IdProducto,
                        IdCategoria = a.IdProductoNavigation.IdCategoria,
                        IdProveedor = a.IdProductoNavigation.IdProveedor,
                        NombreProducto = a.IdProductoNavigation.NombreProducto,
                        PrecioCompra = a.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = a.IdProductoNavigation.PrecioVenta,
                        Stok = a.IdProductoNavigation.Stok,
                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = a.IdUsuarioNavigation.IdUsuario,
                        UserName = a.IdUsuarioNavigation.UserName,
                        Nombre = a.IdUsuarioNavigation.Nombre,
                        Apellido = a.IdUsuarioNavigation.Apellido
                    }
                })
                .ToListAsync();

            return auditoriasFiltrada;
        }

        public async Task RegistrarAuditoria(AuditoriaProducto auditoria)
        {
            var nuevaAuditoria = new EF.AuditoriaProducto()
            {
                IdProducto = auditoria.IdProducto,
                IdUsuario = auditoria.IdUsuario,
                FechaAccion = auditoria.FechaAccion,
                Accion = (byte)auditoria.Accion,
                DatosAnteriores = auditoria.DatosAnteriores,
                DatosNuevos = auditoria.DatosNuevos,
                Descripcion = auditoria.Descripcion
            };

            _context.AuditoriaProductos.Add(nuevaAuditoria);
            await _context.SaveChangesAsync();
        }
    }
}
