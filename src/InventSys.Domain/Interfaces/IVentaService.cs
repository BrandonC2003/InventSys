using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface IVentaService
    {
        Task<List<Venta>> ObtenerVentaAsync(int idVenta);
        Task RegistrarVentaAsync(Venta venta);
    }
}
