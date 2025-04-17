namespace InventSys.Domain.Entities
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string? Telefono { get; set; }
    }
}
