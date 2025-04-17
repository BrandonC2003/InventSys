using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductoPorId(int id);
        Task<Producto> ObtenerProductoPorNombre(string nombreProducto);
        Task<Producto> CrearProducto(Producto producto);
        Task ActualizarProducto(int idProducto, Producto producto);
        Task EliminarProducto(int id);
    }
}
