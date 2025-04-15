using InventSys.Domain.Entities;
using InventSys.Domain.Enums;

namespace InventSys.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuarios>> ObtenerUsuariosAsync();
        Task<Usuarios> ObtenerUsuarioAsync(int userId);
        Task<Usuarios> ObtenerUsuarioAsync(string userName);
        Task CrearUsuarioAsync(Usuarios usuario);
        Task ActualizarUsuarioAsync(int userId, Usuarios usuario);
        Task CambiarEstadoAsync(int userId, UserStatus userStatus);
        Task CambiarClaveAsync(int userId, string newPassword);

    }
}
