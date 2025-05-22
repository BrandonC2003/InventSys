using InventSys.Domain.Entities;
using InventSys.Domain.Exceptions;
using InventSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using EF = InventSys.Infrastructure.Data.EntityFramework;

namespace InventSys.Infrastructure.Services
{
    public class CompraService(EF.InventSysDbContext context, IProductoService productoService) : ICompraService
    {
        private readonly EF.InventSysDbContext _context = context;
        private readonly IProductoService _productoService = productoService;

        public async Task RegistrarCompraAsync(Compra compra)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var compraEntity = new EF.Compra()
                {
                    IdUsuario = compra.IdUsuario,
                    PrecioTotal = compra.PrecioTotal,
                    FechaCompra = compra.FechaCompra,
                    DetalleCompras = [.. compra.DetalleCompras.Select(d => new EF.DetalleCompra()
                    {
                        IdProducto = d.IdProducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    })]
                };

                foreach (var detalleCompra in compraEntity.DetalleCompras)
                {
                    await _productoService.IncrementarStock(detalleCompra.IdProducto, detalleCompra.Cantidad, _context);
                }

                _context.Compras.Add(compraEntity);
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

        public async Task<List<Compra>> ObtenerCompraAsync(int idCompra)
        {
            var compras = await _context.Compras
                .Where(c => c.IdCompra == idCompra)
                .Include(c => c.DetalleCompras)
                .Include(c => c.IdUsuarioNavigation)
                .Select(c => new Compra
                {
                    IdCompra = c.IdCompra,
                    IdUsuario = c.IdUsuario,
                    PrecioTotal = c.PrecioTotal,
                    FechaCompra = c.FechaCompra,
                    DetalleCompras = c.DetalleCompras
                        .Select(d => new DetalleCompra
                        {
                            IdProducto = d.IdProducto,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario
                        }).ToList(),
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = c.IdUsuarioNavigation.IdUsuario,
                        UserName = c.IdUsuarioNavigation.UserName,
                        IdRol = c.IdUsuarioNavigation.IdRol,
                        Nombre = c.IdUsuarioNavigation.Nombre,
                        Apellido = c.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = c.IdUsuarioNavigation.CorreoElectronico
                    }
                }).ToListAsync();

            return compras;
        }
    }
}
