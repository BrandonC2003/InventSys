using System.ComponentModel.DataAnnotations;

namespace InventSys.Application.DTOs
{
    public class CrearProveedorDto
    {
        [Required(ErrorMessage = "Ingresa el nombre dle proveedor")]
        public string NombreProveedor { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El correo ingresado no tiene un formato válido")]
        public string? CorreoElectronico { get; set; }

        public string? Telefono { get; set; }
    }
}
