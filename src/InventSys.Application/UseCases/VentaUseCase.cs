using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class VentaUseCase(IVentaService ventaService)
    {
        private readonly IVentaService _ventaService = ventaService;
        public async Task RegistrarVentaAsync(Domain.Entities.Venta venta)
        {
            await _ventaService.RegistrarVentaAsync(venta);
        }
        public async Task<List<Domain.Entities.Venta>> ObtenerVentasAsync(int idVenta)
        {
            return await _ventaService.ObtenerVentaAsync(idVenta);
        }

        public async Task<List<Domain.Entities.Venta>> ReporteDeVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _ventaService.ReporteDeVentas(fechaInicio, fechaFin);
        }
    }
}
