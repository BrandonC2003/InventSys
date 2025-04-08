using System;
using System.Collections.Generic;

namespace InventSys.Infrastructure.Data.EntityFramework;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public byte Estado { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<AuditoriaProducto> AuditoriaProductos { get; set; } = new List<AuditoriaProducto>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual RolCatalogo IdRolNavigation { get; set; } = null!;

    public virtual ICollection<TrazaAlerta> TrazaAlerta { get; set; } = new List<TrazaAlerta>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
