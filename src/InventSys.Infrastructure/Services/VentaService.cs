using InventSys.Domain.Interfaces;
using InventSys.Domain.Entities;
using InventSys.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using EF = InventSys.Infrastructure.Data.EntityFramework;
namespace InventSys.Infrastructure.Services
{
    public class VentaService(EF.InventSysDbContext context, IProductoService productoService) : IVentaService
    {
        private readonly EF.InventSysDbContext _context = context;
        private readonly IProductoService _productoService = productoService;
        public async Task RegistrarVentaAsync(Venta venta)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var ventaEntity = new EF.Venta()
                {
                    IdUsuario = venta.IdUsuario,
                    PrecioTotal = venta.PrecioTotal,
                    FechaVenta = venta.FechaVenta,
                    DetalleVenta = [.. venta.DetalleVenta.Select(d => new EF.DetalleVentum()
                    {
                        IdProducto = d.IdProducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    })]
                };

                foreach(var detalleVenta in ventaEntity.DetalleVenta)
                {
                    await _productoService.DescontarStockAsync(detalleVenta.IdProducto, detalleVenta.Cantidad, _context);
                }

                _context.Ventas.Add(ventaEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                string errorMessage = "Ocurrió un error al registrar la venta";
                switch (ex)
                {
                    case KeyNotFoundException notFoundException:
                        errorMessage = notFoundException.Message;
                        break;
                    case InvalidOperationException operationException:
                        errorMessage = operationException.Message;
                        break;
                }

                throw new CustomExeption(errorMessage);
            }
        }
        public async Task<List<Venta>> ObtenerVentaAsync(int idVenta)
        {
            var ventas = await _context.Ventas
                .Where(v => v.IdVenta == idVenta)
                .Include(v => v.DetalleVenta)
                .Include(v => v.IdUsuarioNavigation)
                .Select(v => new Venta
                {
                    IdVenta = v.IdVenta,
                    IdUsuario = v.IdUsuario,
                    PrecioTotal = v.PrecioTotal,
                    FechaVenta = v.FechaVenta,
                    DetalleVenta = v.DetalleVenta
                        .Select(d => new DetalleVenta
                        {
                            IdProducto = d.IdProducto,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario
                        }).ToList(),
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = v.IdUsuarioNavigation.IdUsuario,
                        UserName = v.IdUsuarioNavigation.UserName,
                        IdRol = v.IdUsuarioNavigation.IdRol,
                        Nombre = v.IdUsuarioNavigation.Nombre,
                        Apellido = v.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = v.IdUsuarioNavigation.CorreoElectronico
                    }
                }).ToListAsync();

            return ventas;
        }

        public async Task<List<Venta>> ReporteDeVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = await _context.Ventas
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                .Include(v => v.DetalleVenta)
                    .ThenInclude(dv => dv.IdProductoNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .Select(v => new Venta
                {
                    IdVenta = v.IdVenta,
                    IdUsuario = v.IdUsuario,
                    PrecioTotal = v.PrecioTotal,
                    FechaVenta = v.FechaVenta,
                    DetalleVenta = v.DetalleVenta
                        .Select(d => new DetalleVenta
                        {
                            IdVenta = d.IdVenta,
                            IdProducto = d.IdProducto,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario,
                            IdProductoNavigation = new Producto
                            {
                                NombreProducto = d.IdProductoNavigation.NombreProducto
                            }
                        }).ToList(),
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = v.IdUsuarioNavigation.IdUsuario,
                        UserName = v.IdUsuarioNavigation.UserName,
                        IdRol = v.IdUsuarioNavigation.IdRol,
                        Nombre = v.IdUsuarioNavigation.Nombre,
                        Apellido = v.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = v.IdUsuarioNavigation.CorreoElectronico
                    }
                }).ToListAsync();

            return ventas;
        }
    }
}
