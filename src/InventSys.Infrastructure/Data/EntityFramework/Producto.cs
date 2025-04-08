using System;
using System.Collections.Generic;

namespace InventSys.Infrastructure.Data.EntityFramework;

public partial class Producto
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

    public virtual ICollection<AuditoriaProducto> AuditoriaProductos { get; set; } = new List<AuditoriaProducto>();

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<TrazaAlerta> TrazaAlerta { get; set; } = new List<TrazaAlerta>();
}
