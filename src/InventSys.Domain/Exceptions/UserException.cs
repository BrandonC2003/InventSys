
namespace InventSys.Domain.Exceptions
{
    public class UserException(string message) : Exception(message)
    {
        public static UserException PasswordDiferentes() => new("Las contraseñas no coinciden");

        public static UserException UsuarioNoEncontrado() => new("La contraseña del usuario es incorrecta");
    }
}
