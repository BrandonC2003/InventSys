namespace InventSys.Domain.Entities
{
    public class TrazaAlerta
    {
        public int IdTrazaAlerta { get; set; }

        public int IdUsuario { get; set; }

        public int IdProducto { get; set; }

        public byte EstadoAlerta { get; set; }

        public DateTime Fecha { get; set; }

        public string Contenido { get; set; } = null!;

        public virtual Producto IdProductoNavigation { get; set; } = null!;

        public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
    }
}
