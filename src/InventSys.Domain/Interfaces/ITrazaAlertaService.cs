using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
namespace InventSys.Domain.Interfaces
{
    public interface ITrazaAlertaService
    {
        Task<TrazaAlerta> GuardarTrazaAlertaAsync(TrazaAlerta traza);
        Task ActualizarEstadoAlertaAsync(int idTrazaAlerta, EstadoAlerta estadoAlerta);

        Task<TrazaAlerta> ObtenerTrazaAlertaPorIdAsync(int id);
        Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorFechaAsync(DateTime fechaInicio, DateTime FechaFin);
        Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorEstadoAsync(EstadoAlerta estadoAlerta);
        Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorUsuarioAsync(int idUsuario);

    }
}
