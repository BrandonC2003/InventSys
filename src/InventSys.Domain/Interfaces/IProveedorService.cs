using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface IProveedorService
    {
        Task<List<Proveedor>> ObtenerProveedores();
        Task<Proveedor> ObtenerProveedorPorId(int id);
        Task<Proveedor> ObtenerProveedorPorNombre(string nombreProveedor);
        Task CrearProveedor(Proveedor proveedor);
        Task ActualizarProveedor(int idProveedor, Proveedor proveedor);
        Task EliminarProveedor(int id);
    }
}
