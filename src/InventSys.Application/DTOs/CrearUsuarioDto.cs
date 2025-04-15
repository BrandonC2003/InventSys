using System.ComponentModel.DataAnnotations;
namespace InventSys.Application.DTOs
{
    public class CrearUsuarioDto
    {

        [Required(ErrorMessage = "Ingresa el UserName.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecciona el rol del usuario.")]
        [Range(1, 3, ErrorMessage = "Selecciona un rol valido.")]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "Ingresa la contraseña.")]
        [StringLength(30, ErrorMessage = "La contraseña debe ser superior a 8 caracteres.", MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa la contraseña.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa el nombre.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa el apellido.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa el correo electronico.")]
        [EmailAddress(ErrorMessage = "El correo electronico no es valido.")]
        public string Correo { get; set; } = string.Empty;
        
    }
}
