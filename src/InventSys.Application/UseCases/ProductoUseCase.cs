using InventSys.Application.DTOs;
using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Exceptions;
using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class ProductoUseCase(IProductoService productoService, AuditoriaProductoUseCase auditoriaProductoUseCase, UsuarioUseCase usuarioUseCase)
    {
        private readonly IProductoService _productoService = productoService;
        private readonly AuditoriaProductoUseCase _auditoriaProductoUseCase = auditoriaProductoUseCase;
        private readonly UsuarioUseCase _usuarioUseCae = usuarioUseCase;

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _productoService.ObtenerProductos();
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            try
            {
                return await _productoService.ObtenerProductoPorId(id);
            }
            catch (KeyNotFoundException ex)
            {

                throw new CustomExeption(ex.Message);
            }
        }

        public async Task<Producto> ObtenerProductoPorNombre(string nombreProducto)
        {
            try
            {
                return await _productoService.ObtenerProductoPorNombre(nombreProducto);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            //valido que el nombre del producto no exista en la base de datos
            try
            {
                await _productoService.ObtenerProductoPorNombre(producto.NombreProducto);
                throw new CustomExeption("Ya existe un producto con ese nombre");
            }
            catch (KeyNotFoundException)
            {
                //si marca excepción el nombre de producto aún no existe en la base de datos
            }
            var productoCreado = await _productoService.CrearProducto(producto);

            var auditoria = new RegistrarAuditoriaDto()
            {
                IdProducto = productoCreado.IdProducto,
                IdUsuario = await _usuarioUseCae.GetUserIdAsync(),
                Accion = (byte)AccionAuditoria.Creacion,
                DatosAnteriores = new Producto(),
                DatosNuevos = productoCreado
            };

            await _auditoriaProductoUseCase.RegistrarAuditoria(auditoria);

            return productoCreado;
        }

        public async Task<Producto> ActualizarProducto(int idProducto, Producto producto, string razonDeActualizacion)
        {
            var productoExistente = await ObtenerProductoPorId(idProducto);
            producto.IdProducto = idProducto;
            //valido que el nombre del producto no exista en la base de datos
            try
            {
                var productoEncontrado = await _productoService.ObtenerProductoPorNombre(producto.NombreProducto);

                if(productoEncontrado.IdProducto != idProducto)
                    throw new CustomExeption("Ya existe un producto con ese nombre");
            }
            catch (KeyNotFoundException)
            {
                //si marca excepción el nombre de producto aún no existe en la base de datos
            }
            await _productoService.ActualizarProducto(idProducto, producto);
            var auditoria = new RegistrarAuditoriaDto()
            {
                IdProducto = idProducto,
                IdUsuario = await _usuarioUseCae.GetUserIdAsync(),
                Accion = (byte)AccionAuditoria.Modificacion,
                DatosAnteriores = productoExistente,
                DatosNuevos = producto,
                Descripcion = razonDeActualizacion
            };
            await _auditoriaProductoUseCase.RegistrarAuditoria(auditoria);
            return producto;
        }

        public async Task EliminarProducto(int id, string razonDeEliminacion)
        {
            var producto = await ObtenerProductoPorId(id);

            try
            {
                await _productoService.EliminarProducto(id);
                
            }
            catch (DeleteException ex)
            {
                throw new CustomExeption(ex.Message);
            }

            var auditoria = new RegistrarAuditoriaDto()
            {
                IdProducto = id,
                IdUsuario = await _usuarioUseCae.GetUserIdAsync(),
                Accion = (byte)AccionAuditoria.Eliminacion,
                DatosAnteriores = producto,
                DatosNuevos = new Producto(),
                Descripcion = razonDeEliminacion
            };
            await _auditoriaProductoUseCase.RegistrarAuditoria(auditoria);
        }
    }
}
