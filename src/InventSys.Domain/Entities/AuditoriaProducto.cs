namespace InventSys.Domain.Entities
{
    public class AuditoriaProducto
    {
        public int IdAuditoriaProducto { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaAccion { get; set; }

        public byte Accion { get; set; }

        public string DatosAnteriores { get; set; } = null!;

        public string DatosNuevos { get; set; } = null!;

        public string? Descripcion { get; set; }

        public Producto IdProductoNavigation { get; set; } = null!;

        public Usuarios IdUsuarioNavigation { get; set; } = null!;
    }
}
