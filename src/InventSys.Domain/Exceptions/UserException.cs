
namespace InventSys.Domain.Exceptions
{
    public class UserException(string message) : Exception(message)
    {
        public static UserException PasswordDiferentes() => new("Las contraseñas no coinciden");

        public static UserException UsuarioNoEncontrado() => new("No se pudo recuperar el usuario");

        public static UserException ClaveIncorrecta() => new("La contraseña actual es incorrecta.");

        public static UserException PasswordRepetido() => new("La nueva contraseña no puede ser igual a la actual");
        public static UserException NewException(string message) => new(message);
    }
}
