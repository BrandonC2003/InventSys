using InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;
using InventSys.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using EF = InventSys.Infrastructure.Data.EntityFramework;

namespace InventSys.Infrastructure.Services
{
    public class CategoriaService(EF.InventSysDbContext context) : ICategoriaService
    {
        private readonly EF.InventSysDbContext _context = context;
        public async Task ActualizarCategoria(int idCategoria, Categoria categoria)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(idCategoria) ??
                throw new KeyNotFoundException("No existe una categoría con ese id");

            categoriaExistente.NombreCategoria = categoria.NombreCategoria;
            categoriaExistente.Descripcion = categoria.Descripcion;

            _context.Categorias.Update(categoriaExistente);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> CrearCategoria(Categoria categoria)
        {
            var nuevaCategoria = new EF.Categoria()
            {
                NombreCategoria = categoria.NombreCategoria,
                Descripcion = categoria.Descripcion
            };

            _context.Categorias.Add(nuevaCategoria);
            await _context.SaveChangesAsync();

            categoria.IdCategoria = nuevaCategoria.IdCategoria;

            return categoria;
        }

        public async Task EliminarCategoria(int id)
        {
            var categoriaEliminar = await _context.Categorias.FindAsync(id) ??
                throw new KeyNotFoundException("No existe una categoría con ese id");

            _context.Categorias.Remove(categoriaEliminar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw new DeleteException("No se puede eliminar esta categoría por que otros registros dependen de ella");
            }
        }

        public async Task<Categoria> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _context.Categorias
                .Where(c => c.IdCategoria == id)
                .Select(c => new Categoria
                {
                    IdCategoria = c.IdCategoria,
                    NombreCategoria = c.NombreCategoria,
                    Descripcion = c.Descripcion
                }).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No existe una categoría con ese id");

            return categoria;
        }

        public async Task<Categoria> ObtenerCategoriaPorNombre(string nombreCategoria)
        {
            var categoria = await _context.Categorias
                .Where(c => c.NombreCategoria == nombreCategoria)
                .Select(c => new Categoria
                {
                    IdCategoria = c.IdCategoria,
                    NombreCategoria = c.NombreCategoria,
                    Descripcion = c.Descripcion
                }).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("No existe una categoría con ese nombre");

            return categoria;
        }

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            var categorias = await _context.Categorias
                .Select(c => new Categoria
                {
                    IdCategoria = c.IdCategoria,
                    NombreCategoria = c.NombreCategoria,
                    Descripcion = c.Descripcion
                }).ToListAsync();

            return categorias;
        }
    }
}
