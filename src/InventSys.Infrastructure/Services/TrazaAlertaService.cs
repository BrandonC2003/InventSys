using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using EF = InventSys.Infrastructure.Data.EntityFramework;

namespace InventSys.Infrastructure.Services
{
    public class TrazaAlertaService(EF.InventSysDbContext context) : ITrazaAlertaService
    {
        private readonly EF.InventSysDbContext _context = context;

        public async Task ActualizarEstadoAlertaAsync(int idTrazaAlerta, EstadoAlerta estadoAlerta)
        {
            var trazaAlerta = await _context.TrazaAlertas.FindAsync(idTrazaAlerta) ??
                throw new KeyNotFoundException("No existe una traza de alerta con ese id");

            trazaAlerta.EstadoAlerta = (byte)estadoAlerta;
            _context.TrazaAlertas.Update(trazaAlerta);
            await _context.SaveChangesAsync();

        }

        public async Task<TrazaAlerta> GuardarTrazaAlertaAsync(TrazaAlerta traza)
        {
            var nuevaAlerta = new EF.TrazaAlerta()
            {
                IdUsuario = traza.IdUsuario,
                IdProducto = traza.IdProducto,
                EstadoAlerta = (byte)traza.EstadoAlerta,
                Fecha = traza.Fecha,
                Contenido = traza.Contenido
            };

            _context.TrazaAlertas.Add(nuevaAlerta);
            await _context.SaveChangesAsync();

            traza.IdTrazaAlerta = nuevaAlerta.IdTrazaAlerta;
            return traza;
        }

        public async Task<TrazaAlerta> ObtenerTrazaAlertaPorIdAsync(int id)
        {
            return await _context.TrazaAlertas
                .Where(t => t.IdTrazaAlerta == id)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .Select(t => new TrazaAlerta()
                {
                    IdTrazaAlerta = t.IdTrazaAlerta,
                    IdUsuario = t.IdUsuario,
                    IdProducto = t.IdProducto,
                    EstadoAlerta = t.EstadoAlerta,
                    Fecha = t.Fecha,
                    Contenido = t.Contenido,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = t.IdProductoNavigation.IdProducto,
                        NombreProducto = t.IdProductoNavigation.NombreProducto,
                        Descripcion = t.IdProductoNavigation.Descripcion,
                        PrecioCompra = t.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = t.IdProductoNavigation.PrecioVenta,
                        Stok = t.IdProductoNavigation.Stok,

                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = t.IdUsuarioNavigation.IdUsuario,
                        UserName = t.IdUsuarioNavigation.UserName,
                        Nombre = t.IdUsuarioNavigation.Nombre,
                        Apellido = t.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = t.IdUsuarioNavigation.CorreoElectronico
                    }
                })
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No existe una traza de alerta con ese id");
        }

        public async Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorEstadoAsync(EstadoAlerta estadoAlerta)
        {
            return await _context.TrazaAlertas
                .Where(t => t.EstadoAlerta == Convert.ToByte(estadoAlerta))
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .Select(t => new TrazaAlerta()
                {
                    IdTrazaAlerta = t.IdTrazaAlerta,
                    IdUsuario = t.IdUsuario,
                    IdProducto = t.IdProducto,
                    EstadoAlerta = t.EstadoAlerta,
                    Fecha = t.Fecha,
                    Contenido = t.Contenido,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = t.IdProductoNavigation.IdProducto,
                        NombreProducto = t.IdProductoNavigation.NombreProducto,
                        Descripcion = t.IdProductoNavigation.Descripcion,
                        PrecioCompra = t.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = t.IdProductoNavigation.PrecioVenta,
                        Stok = t.IdProductoNavigation.Stok,

                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = t.IdUsuarioNavigation.IdUsuario,
                        UserName = t.IdUsuarioNavigation.UserName,
                        Nombre = t.IdUsuarioNavigation.Nombre,
                        Apellido = t.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = t.IdUsuarioNavigation.CorreoElectronico
                    }
                })
                .ToListAsync() ?? throw new KeyNotFoundException("No existe ninguna traza de alerta con ese estado");
        }

        public async Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorFechaAsync(DateTime fechaInicio, DateTime FechaFin)
        {
            return await _context.TrazaAlertas
                .Where(t => t.Fecha >= fechaInicio && t.Fecha <= FechaFin)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .Select(t => new TrazaAlerta()
                {
                    IdTrazaAlerta = t.IdTrazaAlerta,
                    IdUsuario = t.IdUsuario,
                    IdProducto = t.IdProducto,
                    EstadoAlerta = t.EstadoAlerta,
                    Fecha = t.Fecha,
                    Contenido = t.Contenido,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = t.IdProductoNavigation.IdProducto,
                        NombreProducto = t.IdProductoNavigation.NombreProducto,
                        Descripcion = t.IdProductoNavigation.Descripcion,
                        PrecioCompra = t.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = t.IdProductoNavigation.PrecioVenta,
                        Stok = t.IdProductoNavigation.Stok,

                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = t.IdUsuarioNavigation.IdUsuario,
                        UserName = t.IdUsuarioNavigation.UserName,
                        Nombre = t.IdUsuarioNavigation.Nombre,
                        Apellido = t.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = t.IdUsuarioNavigation.CorreoElectronico
                    }
                })
                .ToListAsync() ?? throw new KeyNotFoundException("No existe ninguna traza de alerta en ese rango de fechas");
        }

        public async Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorUsuarioAsync(int idUsuario)
        {
            return await _context.TrazaAlertas
                .Where(t => t.IdUsuario == idUsuario)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .Select(t => new TrazaAlerta()
                {
                    IdTrazaAlerta = t.IdTrazaAlerta,
                    IdUsuario = t.IdUsuario,
                    IdProducto = t.IdProducto,
                    EstadoAlerta = t.EstadoAlerta,
                    Fecha = t.Fecha,
                    Contenido = t.Contenido,
                    IdProductoNavigation = new Producto()
                    {
                        IdProducto = t.IdProductoNavigation.IdProducto,
                        NombreProducto = t.IdProductoNavigation.NombreProducto,
                        Descripcion = t.IdProductoNavigation.Descripcion,
                        PrecioCompra = t.IdProductoNavigation.PrecioCompra,
                        PrecioVenta = t.IdProductoNavigation.PrecioVenta,
                        Stok = t.IdProductoNavigation.Stok,

                    },
                    IdUsuarioNavigation = new Usuarios()
                    {
                        IdUsuario = t.IdUsuarioNavigation.IdUsuario,
                        UserName = t.IdUsuarioNavigation.UserName,
                        Nombre = t.IdUsuarioNavigation.Nombre,
                        Apellido = t.IdUsuarioNavigation.Apellido,
                        CorreoElectronico = t.IdUsuarioNavigation.CorreoElectronico
                    }
                })
                .ToListAsync() ?? throw new KeyNotFoundException("No existe ninguna traza de alerta asociada a ese usuario");
        }
    }
}
