using InventSys.Domain.Interfaces;
using InventSys.Domain.Entities;
using InventSys.Domain.Exceptions;

namespace InventSys.Application.UseCases
{
    public class CategoriaUseCase(ICategoriaService categoriaService)
    {
        private readonly ICategoriaService _categoriaService = categoriaService;

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            return await _categoriaService.ObtenerCategorias();
        }

        public async Task<Categoria> ObtenerCategoriaPorId(int id)
        {
            try
            {
                return await _categoriaService.ObtenerCategoriaPorId(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
        }

        public async Task<Categoria> ObtenerCategoriaPorNombre(string nombreCategoria)
        {
            try
            {
                return await _categoriaService.ObtenerCategoriaPorNombre(nombreCategoria);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
        }

        public async Task CrearCategoria(Categoria categoria)
        {
            try
            {
                //Retorna excepción si la categoria no existe
                await _categoriaService.ObtenerCategoriaPorNombre(categoria.NombreCategoria);
                throw new CustomExeption("Ya existe una categoria con ese nombre");
            }
            catch (KeyNotFoundException)
            {
                //si la categoria no existe procedemos a realizar el guardado
            }
            await _categoriaService.CrearCategoria(categoria);
        }

        public async Task ActualizarCategoria(int idCategoria, Categoria categoria)
        {
            try
            {
                //Valida que el nombre no se repita en otra categoria que no sea el que se está actualizando
                var categoriaEncontrada = await _categoriaService.ObtenerCategoriaPorNombre(categoria.NombreCategoria);
                if(idCategoria != categoriaEncontrada.IdCategoria)
                    throw new CustomExeption("Ya existe una categoria con ese nombre");
            }
            catch (KeyNotFoundException)
            {
                //si la categoria no existe procedemos a realizar la actualización
            }

            try
            {
                await _categoriaService.ActualizarCategoria(idCategoria, categoria);
            }
            catch (KeyNotFoundException ex)
            {

                throw new CustomExeption(ex.Message);
            }
        }

        public async Task EliminarCategoria(int id)
        {
            try
            {
                await _categoriaService.EliminarCategoria(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
        }
    }
}
