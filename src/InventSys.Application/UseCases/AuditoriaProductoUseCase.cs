using InventSys.Application.DTOs;
using InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;
using InventSys.Domain.Tools;

namespace InventSys.Application.UseCases
{
    public class AuditoriaProductoUseCase(IAuditoriaProductoService auditoriaProductoService)
    {
        private readonly IAuditoriaProductoService _auditoriaProductoService = auditoriaProductoService;

        public async Task RegistrarAuditoria(RegistrarAuditoriaDto registroAuditoria)
        {
            var auditoria = new AuditoriaProducto
            {
                IdAuditoriaProducto = registroAuditoria.IdAuditoriaProducto,
                IdProducto = registroAuditoria.IdProducto,
                IdUsuario = registroAuditoria.IdUsuario,
                FechaAccion = registroAuditoria.FechaAccion,
                Accion = registroAuditoria.Accion,
                DatosAnteriores = XmlTools.Serializar<Producto>(registroAuditoria.DatosAnteriores),
                DatosNuevos = XmlTools.Serializar<Producto>(registroAuditoria.DatosNuevos),
                Descripcion = registroAuditoria.Descripcion
            };

            await _auditoriaProductoService.RegistrarAuditoria(auditoria);
        }


    }
}
