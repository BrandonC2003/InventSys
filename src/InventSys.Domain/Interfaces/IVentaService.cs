using InventSys.Domain.Entities;
using InventSys.Domain.Exceptions;
namespace InventSys.Domain.Interfaces
{
    public interface IVentaService
    {
        Task<List<Venta>> ObtenerVentaAsync(int idVenta);

        Task<List<Venta>> ReporteDeVentas(DateTime fechaInicio, DateTime fechaFin);
        /// <summary>
        /// Registra una venta en la base de datos y actualiza el stock de los productos vendidos.
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        /// <exception cref="CustomExeption"></exception>
        Task RegistrarVentaAsync(Venta venta);
    }
}
