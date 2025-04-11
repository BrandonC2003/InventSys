using System.ComponentModel.DataAnnotations;
namespace InventSys.Application.DTOs
{
    public class LogInDto
    {
        [Required(ErrorMessage = "Ingresa tu nombre de usuario")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Debes ingresar tu contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
