namespace InventSys.Domain.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public int IdCategoria { get; set; }

        public int IdProveedor { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stok { get; set; }

        public int StokBajo { get; set; }

        public string MensajeAlerta { get; set; } = null!;
        public Categoria IdCategoriaNavigation { get; set; } = null!;

        public Proveedor IdProveedorNavigation { get; set; } = null!;
    }
}
