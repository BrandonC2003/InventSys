using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class TrazaAlertaUseCase (ITrazaAlertaService trazaAlertaService)
    {
        private readonly ITrazaAlertaService _trazaAlertaService = trazaAlertaService;

        public async Task<TrazaAlerta> GuardarTrazaAlertaAsync(TrazaAlerta trazaAlerta)
        {
            return await _trazaAlertaService.GuardarTrazaAlertaAsync(trazaAlerta);
        }

        public async Task ActualizarEstadoAlertaAsync(int idTraza, EstadoAlerta estadoAlerta)
        {
            await _trazaAlertaService.ActualizarEstadoAlertaAsync(idTraza, estadoAlerta);
        }

        public async Task<TrazaAlerta> ObtenerTrazaAlertaPorIdAsync(int id)
        {
            return await _trazaAlertaService.ObtenerTrazaAlertaPorIdAsync(id);
        }
        public async Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorFechaAsync(DateTime fechaInicio, DateTime FechaFin)
        {
            return await _trazaAlertaService.ObtenerTrazaAlertasPorFechaAsync(fechaInicio, FechaFin);
        }
        public async Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorEstadoAsync(EstadoAlerta estadoAlerta)
        {
            return await _trazaAlertaService.ObtenerTrazaAlertasPorEstadoAsync(estadoAlerta);
        }
        public async Task<List<TrazaAlerta>> ObtenerTrazaAlertasPorUsuarioAsync(int idUsuario)
        {
            return await _trazaAlertaService.ObtenerTrazaAlertasPorUsuarioAsync(idUsuario);
        }
    }
}
