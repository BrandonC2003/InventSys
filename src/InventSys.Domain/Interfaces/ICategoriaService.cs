using InventSys.Domain.Entities;

namespace InventSys.Domain.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObtenerCategorias();
        Task<Categoria> ObtenerCategoriaPorId(int id);
        Task<Categoria> ObtenerCategoriaPorNombre(string nombreCategoria);
        Task CrearCategoria(Categoria categoria);
        Task ActualizarCategoria(int idCategoria, Categoria categoria);
        Task EliminarCategoria(int id);

    }
}
