namespace InventSys.Domain.Entities
{
    public class Venta
    {
        public int IdVenta { get; set; }

        public int IdUsuario { get; set; }

        public decimal PrecioTotal { get; set; }

        public DateTime FechaVenta { get; set; }

        public List<DetalleVenta> DetalleVenta { get; set; } = [];
        public Usuarios IdUsuarioNavigation { get; set; } = null!;

    }
}
