using InventSys.Domain.Interfaces;
using InventSys.Domain.Entities;
using InventSys.Domain.Exceptions;
namespace InventSys.Application.UseCases
{
    
    public class ProveedorUseCase(IProveedorService proveedorService)
    {
        private readonly IProveedorService _proveedorService = proveedorService;

        public async Task<List<Proveedor>> ObtenerProveedores()
        {
            return await _proveedorService.ObtenerProveedores();
        }

        public async Task<Proveedor> ObtenerProveedorPorId(int id)
        {
            try
            {
                return await _proveedorService.ObtenerProveedorPorId(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
            
        }

        public async Task<Proveedor> ObtenerProveedorPorNombre(string nombreProveedor)
        {
            try
            {
                return await _proveedorService.ObtenerProveedorPorNombre(nombreProveedor);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
        }

        public async Task CrearProveedor(Proveedor proveedor)
        {
            try
            {
                //Retorna excepción si el proveedor no existe
                await _proveedorService.ObtenerProveedorPorNombre(proveedor.NombreProveedor);
                throw new CustomExeption("Ya existe un proveedor con ese nombre");
            }
            catch (KeyNotFoundException)
            {
                //si el proveedor no existe procedemos a realizar el guardado
            }

            await _proveedorService.CrearProveedor(proveedor);
        }

        public async Task ActualizarProveedor(int idProveedor, Proveedor proveedor)
        {
            try
            {
                //Valida que el nombre no se repita en otro proveedor que no sea el que se está actualizando
                var proveedorEncontrado = await _proveedorService.ObtenerProveedorPorNombre(proveedor.NombreProveedor);
                if (proveedorEncontrado.IdProveedor == idProveedor )
                    throw new CustomExeption("Ya existe un proveedor con ese nombre");
            }
            catch (KeyNotFoundException)
            {
                //si el proveedor no existe procedemos a realizar el guardado
            }

            try
            {
                await _proveedorService.ActualizarProveedor(idProveedor, proveedor);
            }
            catch (KeyNotFoundException ex)
            {
                throw new CustomExeption(ex.Message);
            }
        }

        public async Task EliminarProveedor(int id)
        {
            try
            {
                await _proveedorService.EliminarProveedor(id);
            }
            catch (KeyNotFoundException ex)
            {

                throw new CustomExeption(ex.Message);
            }
        }
    }
}
