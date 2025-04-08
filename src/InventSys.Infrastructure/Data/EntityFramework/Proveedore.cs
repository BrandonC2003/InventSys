using System;
using System.Collections.Generic;

namespace InventSys.Infrastructure.Data.EntityFramework;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
