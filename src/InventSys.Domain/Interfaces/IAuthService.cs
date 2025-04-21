namespace InventSys.Domain.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Id del usuario que inició sesión, si retorna -1 significa que no inició sesión</returns>
        Task<int> IniciarSesionAsync(string userName, string password);
        Task<bool> CerrarSesionAsync(int userId);

        Task DarAccesoTemporalAsync();
    }
}
