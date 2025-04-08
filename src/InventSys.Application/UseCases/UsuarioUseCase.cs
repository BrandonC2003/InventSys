using InventSys.Application.DTOs;
using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Exceptions;
using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class UsuarioUseCas(IUsuarioService usuarioService, 
        IAuthService authService, 
        IEncryptService encryptService)
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly IAuthService _authService = authService;
        private readonly IEncryptService _encryptService = encryptService;

        public async Task<bool> IniciarSesionAsync(LogInDto logInDto)
        {
            logInDto.Password = await _encryptService.EncryptAsync(logInDto.Password);

            return await _authService.IniciarSesionAsync(logInDto.UserName, logInDto.Password);
        }

        public async Task<bool> CerrarSesionAsync()
        {
            int userId = await _authService.ObtenerIdUsuario();
            return await _authService.CerrarSesionAsync(userId);
        }

        public async Task<bool> PerteneceRol(string rolName)
        {
            int userId = await _authService.ObtenerIdUsuario();
            return await _authService.PerteneceRol(userId, rolName);
        }

        public async Task<List<Usuarios>> ObtenerUsuariosAsync()
        {
            return await _usuarioService.ObtenerUsuariosAsync();
        }

        public async Task<Usuarios> ObtenerUsuarioAsync(int userId)
        {
            return await _usuarioService.ObtenerUsuarioAsync(userId);
        }

        public async Task CrearUsuarioAsync(UsuarioDto usuarioDto)
        {
            if (usuarioDto.NuevoPassword != usuarioDto.NuevoPassword2)
            {
                throw UserException.PasswordDiferentes();
            }
            var usuario = new Usuarios
            {
                UserName = usuarioDto.UserName,
                Password = await _encryptService.EncryptAsync(usuarioDto.NuevoPassword),
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.Correo,
                Estado = 0,
                Activo = true
            };
            await _usuarioService.CrearUsuarioAsync(usuario);
        }

        public async Task ActualizarUsuarioAsync(UsuarioDto usuarioDto)
        {
            int userId = await _authService.ObtenerIdUsuario();

            var usuario = new Usuarios
            {
                UserName = usuarioDto.UserName,
                Password = string.Empty, //El método no actualiza contraseña
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.Correo,
                Estado = 0,
                Activo = usuarioDto.Activo
            };

            await _usuarioService.ActualizarUsuarioAsync(userId, usuario);
        }

        public async Task CambiarEstadoAsync(UserStatus userStatus)
        {
            int userId = await _authService.ObtenerIdUsuario();
            await _usuarioService.CambiarEstadoAsync(userId, userStatus);
        }

        public async Task CambiarClaveAsync(UsuarioDto usuarioDto)
        {
            int userId = await _authService.ObtenerIdUsuario();

            var usuario = await _usuarioService.ObtenerUsuarioAsync(userId);

            if (usuario == null || usuario.Password != await _encryptService.EncryptAsync(usuarioDto.Password))
            {
                throw UserException.UsuarioNoEncontrado();
            }

            var encryptedPassword = await _encryptService.EncryptAsync(usuarioDto.NuevoPassword);
            await _usuarioService.CambiarClaveAsync(userId, encryptedPassword);
        }
    }
}
