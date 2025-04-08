using System;
using System.Collections.Generic;

namespace InventSys.Infrastructure.Data.EntityFramework;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int IdUsuario { get; set; }

    public decimal PrecioTotal { get; set; }

    public DateTime FechaCompra { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
