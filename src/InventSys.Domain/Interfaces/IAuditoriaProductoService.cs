using InventSys.Domain.Entities;
using InventSys.Domain.Enums;

namespace InventSys.Domain.Interfaces
{
    public interface IAuditoriaProductoService
    {
        Task<List<AuditoriaProducto>> ObtenerAuditoriaPorFecha(DateTime inicio, DateTime fin);
        Task<List<AuditoriaProducto>> ObtenerAuditoriaPorAccion(AccionAuditoria accion);
        Task<List<AuditoriaProducto>> ObtenerAuditoriaPorProducto(int productoId);
        Task<List<AuditoriaProducto>> ObtenerAuditoriaPorUsuario(int usuarioId);
        Task RegistrarAuditoria(AuditoriaProducto auditoria);
    }
}
