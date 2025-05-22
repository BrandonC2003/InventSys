using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface ICompraService
    {
        Task<List<Compra>> ObtenerCompraAsync(int idCompra);

        /// <summary>
        /// Registra una venta en la base de datos y actualiza el stock de los productos vendidos.
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        /// <exception cref="CustomExeption"></exception>
        Task RegistrarCompraAsync(Compra compra);
    }
}
