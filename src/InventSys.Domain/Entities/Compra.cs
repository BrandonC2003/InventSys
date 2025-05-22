namespace InventSys.Domain.Entities
{
    public class Compra
    {
        public int IdCompra { get; set; }

        public int IdUsuario { get; set; }

        public decimal PrecioTotal { get; set; }

        public DateTime FechaCompra { get; set; }

        public string? Descripcion { get; set; }

        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
        public Usuarios IdUsuarioNavigation { get; set; } = null!;
    }
}
