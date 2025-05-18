using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Exceptions;
using InventSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using EF = InventSys.Infrastructure.Data.EntityFramework;

namespace InventSys.Infrastructure.Services
{
    public class ProductoService(EF.InventSysDbContext context) : IProductoService
    {
        private readonly EF.InventSysDbContext _context = context;

        public async Task ActualizarProducto(int idProducto, Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(idProducto) ??
                throw new KeyNotFoundException("No se encontró un producto con ese id");
            productoExistente.IdCategoria = producto.IdCategoria;
            productoExistente.IdProveedor = producto.IdProveedor;
            productoExistente.NombreProducto = producto.NombreProducto;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.PrecioCompra = producto.PrecioCompra;
            productoExistente.PrecioVenta = producto.PrecioVenta;
            productoExistente.Stok = producto.Stok;
            productoExistente.StokBajo = producto.StokBajo;
            productoExistente.MensajeAlerta = producto.MensajeAlerta;

            _context.Productos.Update(productoExistente);
            await _context.SaveChangesAsync();
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            var nuevoProducto = new EF.Producto()
            {
                IdCategoria = producto.IdCategoria,
                IdProveedor = producto.IdProveedor,
                NombreProducto = producto.NombreProducto,
                Descripcion = producto.Descripcion,
                PrecioCompra = producto.PrecioCompra,
                PrecioVenta = producto.PrecioVenta,
                Stok = producto.Stok,
                StokBajo = producto.StokBajo,
                MensajeAlerta = producto.MensajeAlerta,
            };

            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();

            producto.IdProducto = nuevoProducto.IdProducto;

            return producto;
        }

        
        public async Task DescontarStockAsync(int idProducto, int cantidad, object? dbContext = null)
        {
            EF.InventSysDbContext dbContext1 = dbContext as EF.InventSysDbContext ?? _context;

            var producto = await dbContext1.Productos.FindAsync(idProducto) ??
                throw new KeyNotFoundException("No se encontró un producto con ese id");

            if (producto.Stok < cantidad)
                throw new InvalidOperationException("No hay suficiente stock para descontar la cantidad solicitada");

            producto.Stok -= cantidad;

            dbContext1.Productos.Update(producto);
            await dbContext1.SaveChangesAsync();
        }

        public async Task EliminarProducto(int id)
        {
            var productoEliminar = await _context.Productos.FindAsync(id) ??
                throw new KeyNotFoundException("No se encontró un producto con ese id");

            _context.Productos.Remove(productoEliminar);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw new DeleteException("El producto no se puede eliminar por que hay otros registros que dependen de él");
            }
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            var productos = await _context.Productos
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.IdProducto == id)
                .Select(p => new Producto
                {
                    IdProducto = p.IdProducto,
                    IdCategoria = p.IdCategoria,
                    IdProveedor = p.IdProveedor,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    Stok = p.Stok,
                    StokBajo = p.StokBajo,
                    MensajeAlerta = p.MensajeAlerta,
                    IdCategoriaNavigation = new Categoria
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria
                    },
                    IdProveedorNavigation = new Proveedor
                    {
                        IdProveedor = p.IdProveedorNavigation.IdProveedor,
                        NombreProveedor = p.IdProveedorNavigation.NombreProveedor
                    }
                }).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No se encontró un producto con ese id");

            return productos;
        }

        public async Task<Producto> ObtenerProductoPorNombre(string nombreProducto)
        {
            var productos = await _context.Productos
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.NombreProducto == nombreProducto)
                .Select(p => new Producto
                {
                    IdProducto = p.IdProducto,
                    IdCategoria = p.IdCategoria,
                    IdProveedor = p.IdProveedor,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    Stok = p.Stok,
                    StokBajo = p.StokBajo,
                    MensajeAlerta = p.MensajeAlerta,
                    IdCategoriaNavigation = new Categoria
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria
                    },
                    IdProveedorNavigation = new Proveedor
                    {
                        IdProveedor = p.IdProveedorNavigation.IdProveedor,
                        NombreProveedor = p.IdProveedorNavigation.NombreProveedor
                    }
                }).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No se encontró un producto con ese nombre");

            return productos;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Select(p => new Producto
                {
                    IdProducto = p.IdProducto,
                    IdCategoria = p.IdCategoria,
                    IdProveedor = p.IdProveedor,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    Stok = p.Stok,
                    StokBajo = p.StokBajo,
                    MensajeAlerta = p.MensajeAlerta,
                    IdCategoriaNavigation = new Categoria
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria
                    },
                    IdProveedorNavigation = new Proveedor
                    {
                        IdProveedor = p.IdProveedorNavigation.IdProveedor,
                        NombreProveedor = p.IdProveedorNavigation.NombreProveedor
                    }
                }).ToListAsync();

            return productos;
        }

        public async Task<List<Producto>> ObtenerProductosConStokBajo()
        {
            var productosStokBajo = await _context.Productos
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Stok <= p.StokBajo)
                .Select(p => new Producto
                {
                    IdProducto = p.IdProducto,
                    IdCategoria = p.IdCategoria,
                    IdProveedor = p.IdProveedor,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    Stok = p.Stok,
                    StokBajo = p.StokBajo,
                    MensajeAlerta = p.MensajeAlerta,
                    IdCategoriaNavigation = new Categoria
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria
                    },
                    IdProveedorNavigation = new Proveedor
                    {
                        IdProveedor = p.IdProveedorNavigation.IdProveedor,
                        NombreProveedor = p.IdProveedorNavigation.NombreProveedor
                    }
                }).ToListAsync();
            return productosStokBajo;
        }

        public async Task<bool> TieneAlertas(int idProducto, int minutosAConsiderar)
        {
            var tieneAlertas = await _context.TrazaAlertas
                .AnyAsync(t => t.IdProducto == idProducto &&
                    t.Fecha >= DateTime.Now.AddMinutes(-minutosAConsiderar) &&
                    t.EstadoAlerta == (byte)EstadoAlerta.Enviada);

            return tieneAlertas;
        }
    }
}
