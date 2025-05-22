using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class CompraUseCase(ICompraService compraService)
    {
        ICompraService _compraService = compraService;

        public async Task RegistrarCompraAsync(Domain.Entities.Compra compra)
        {
            await _compraService.RegistrarCompraAsync(compra);
        }
        public async Task<List<Domain.Entities.Compra>> ObtenerVentasAsync(int idCompra)
        {
            return await _compraService.ObtenerCompraAsync(idCompra);
        }
    }
}
