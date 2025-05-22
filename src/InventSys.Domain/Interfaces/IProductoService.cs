using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductos();
        Task<List<Producto>> ObtenerProductosConStokBajo();
        Task<Producto> ObtenerProductoPorId(int id);
        Task<Producto> ObtenerProductoPorNombre(string nombreProducto);
        Task<Producto> CrearProducto(Producto producto);
        Task ActualizarProducto(int idProducto, Producto producto);
        Task EliminarProducto(int id);

        /// <summary>
        /// Descontar stock de un producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        /// <param name="dbContext">Contexto de datos (puede ser nulo) para que funcione correctamente dentro de transacciones externas</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Cuando no se encuentra un producto con el id especificado</exception>
        /// <exception cref="InvalidOperationException">Cuando la cantidad a descontar es mayor a la cantidad existente</exception>
        Task DescontarStockAsync(int idProducto, int cantidad, object? dbContext = null);
        Task IncrementarStock(int idProducto, int cantidad, object? dbContext = null);

        /// <summary>
        /// Valida si el producto tiene alertas en los  minutosAConsiderar
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="minutosAConsiderar"></param>
        /// <returns></returns>
        Task<bool> TieneAlertas(int idProducto, int minutosAConsiderar);
    }
}
