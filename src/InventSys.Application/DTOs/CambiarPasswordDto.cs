using System.ComponentModel.DataAnnotations;

namespace InventSys.Application.DTOs
{
    public class CambiarPasswordDto
    {

        [Required(ErrorMessage = "Ingresa la contraseña actual")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa la nueva contraseña.")]
        public string NuevoPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repite la nueva contraseña.")]
        [Compare("NuevoPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string NuevoPasswordRepeat { get; set; } = string.Empty;
    }
}
