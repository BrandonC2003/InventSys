namespace InventSys.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<bool> IniciarSesionAsync(string userName, string password);
        Task<bool> CerrarSesionAsync(int userId);
        Task<bool> PerteneceRol(int userId, string rolName);
        Task<int> ObtenerIdUsuario();
    }
}
