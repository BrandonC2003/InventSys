using InventSys.Application.DTOs;
using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Exceptions;
using InventSys.Domain.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace InventSys.Application.UseCases
{
    public class UsuarioUseCase(IUsuarioService usuarioService, 
        IAuthService authService, 
        IEncryptService encryptService,
        AuthenticationStateProvider authProvider)
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly IAuthService _authService = authService;
        private readonly IEncryptService _encryptService = encryptService;
        private readonly AuthenticationStateProvider _authProvider = authProvider;

        public async Task<bool> IniciarSesionAsync(LogInDto logInDto)
        {
            string encryptedPassword = await _encryptService.EncryptAsync(logInDto.Password);

            return await _authService.IniciarSesionAsync(logInDto.UserName, encryptedPassword);
        }

        public async Task<int> GetUserIdAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            
            if (!int.TryParse(authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                throw UserException.UsuarioNoEncontrado();
            }
            return userId;
        }

        public async Task<bool> CerrarSesionAsync()
        {
            int userId = await GetUserIdAsync();
            return await _authService.CerrarSesionAsync(userId);
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
                IdRol  = usuarioDto.IdRol,
                Estado = 0,
                Activo = true
            };
            await _usuarioService.CrearUsuarioAsync(usuario);
        }

        public async Task ActualizarUsuarioAsync(int userId, UsuarioDto usuarioDto)
        {

            var usuario = new Usuarios
            {
                UserName = usuarioDto.UserName,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.Correo,
                Activo = usuarioDto.Activo
            };

            await _usuarioService.ActualizarUsuarioAsync(userId, usuario);
        }

        public async Task CambiarEstadoAsync(int userId,UserStatus userStatus)
        {
            await _usuarioService.CambiarEstadoAsync(userId, userStatus);
        }

        public async Task CambiarClaveAsync(UsuarioDto usuarioDto, int userId)
        {

            var usuario = await _usuarioService.ObtenerUsuarioAsync(userId);

            if (usuario == null || usuario.Password != await _encryptService.EncryptAsync(usuarioDto.Password))
            {
                throw UserException.ClaveIncorrecta();
            }

            if (usuarioDto.NuevoPassword != usuarioDto.NuevoPassword2)
            {
                throw UserException.PasswordDiferentes();
            }

            if (usuario.Password == usuarioDto.NuevoPassword)
            {
                throw UserException.PasswordRepetido();
            }

            usuarioDto.NuevoPassword = await _encryptService.EncryptAsync(usuarioDto.NuevoPassword);

            var encryptedPassword = await _encryptService.EncryptAsync(usuarioDto.NuevoPassword);
            await _usuarioService.CambiarClaveAsync(userId, encryptedPassword);
        }
    }
}
