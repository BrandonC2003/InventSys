using System;
using System.Collections.Generic;

namespace InventSys.Infrastructure.Data.EntityFramework;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int IdUsuario { get; set; }

    public decimal PrecioTotal { get; set; }

    public DateTime FechaVenta { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
