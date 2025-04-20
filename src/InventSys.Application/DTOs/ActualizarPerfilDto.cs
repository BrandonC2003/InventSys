using System.ComponentModel.DataAnnotations;

namespace InventSys.Application.DTOs
{
    public class ActualizarPerfilDto
    {
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingresa tu nombre")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingresa tu apellido")]
        public string Apellido { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingresa tu correo electrónico")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
