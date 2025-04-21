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

        public async Task<int> IniciarSesionAsync(LogInDto logInDto)
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
            await CambiarEstadoAsync(userId, UserStatus.FueraSistema);
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

        public async Task<Usuarios> CrearUsuarioAsync(CrearUsuarioDto usuarioDto)
        {
            if (usuarioDto.Password != usuarioDto.PasswordConfirmation)
            {
                throw UserException.PasswordDiferentes();
            }

            //Valida que el nombre de usuario no exista en la base de datos
            try
            {
                await _usuarioService.ObtenerUsuarioAsync(usuarioDto.UserName);
                throw UserException.NewException("El nombre de usuario ya está en uso.");
            }
            catch (KeyNotFoundException)
            {

                //si marca excepción el nombre de usuario aún no existe en la base de datos
            }

            var usuario = new Usuarios
            {
                UserName = usuarioDto.UserName,
                Password = await _encryptService.EncryptAsync(usuarioDto.PasswordConfirmation),
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.Correo,
                IdRol  = usuarioDto.IdRol,
                Estado = 0,
                Activo = true
            };

            await _usuarioService.CrearUsuarioAsync(usuario);

            return await _usuarioService.ObtenerUsuarioAsync(usuario.UserName);
        }

        public async Task ActualizarUsuarioAsync(int userId, Usuarios usuario)
        {

            await _usuarioService.ActualizarUsuarioAsync(userId, usuario);
        }

        public async Task CambiarEstadoAsync(int userId,UserStatus userStatus)
        {
            await _usuarioService.CambiarEstadoAsync(userId, userStatus);
        }

        public async Task CambiarClaveAsync(CambiarPasswordDto passwordDto, int userId)
        {

            var usuario = await _usuarioService.ObtenerUsuarioAsync(userId);

            if (usuario == null || usuario.Password != await _encryptService.EncryptAsync(passwordDto.CurrentPassword))
            {
                throw UserException.ClaveIncorrecta();
            }

            if (passwordDto.NuevoPassword != passwordDto.NuevoPasswordRepeat)
            {
                throw UserException.PasswordDiferentes();
            }

            if (usuario.Password == passwordDto.NuevoPassword)
            {
                throw UserException.PasswordRepetido();
            }

            var encryptedPassword = await _encryptService.EncryptAsync(passwordDto.NuevoPassword);
            await _usuarioService.CambiarClaveAsync(userId, encryptedPassword);
        }

        public bool IsInRol(string? rol)
        {
            var authState = authProvider.GetAuthenticationStateAsync().Result;
            var user = authState.User;
            string? rolUsuario = string.Empty;

            if (user.Identity?.IsAuthenticated == true)
            {
                rolUsuario = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            }

            return rolUsuario == rol;
        }

        public async Task<bool> HayUsuariosRegistrado()
        {
            return await _usuarioService.HayUsuariosRegistrado();
        }

        public async Task DarAccesoTemporalAsync()
        {
            await _authService.DarAccesoTemporalAsync();
        }
    }
}
