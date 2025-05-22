namespace InventSys.Domain.Entities
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }

        public int IdCompra { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
        public Producto IdProductoNavigation { get; set; } = null!;
    }
}
