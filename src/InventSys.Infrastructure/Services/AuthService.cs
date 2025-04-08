using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventSys.Infrastructure.Services
{
    public class AuthService(IHttpContextAccessor httpContextAccessor, InventSysDbContext context) : IAuthService
    {
        private readonly InventSysDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<bool> CerrarSesionAsync(int userId)
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }

        public async Task<bool> IniciarSesionAsync(string userName, string password)
        {
            // Validar las credenciales del usuario
            var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);

            if (usuario == null || !usuario.Activo)
                return false;

            // Crear los claims para el usuario autenticado
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.UserName),
                new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new("FullName", $"{usuario.Nombre} {usuario.Apellido}"),
                new(ClaimTypes.Role, usuario.IdRol.ToString()) // Asumiendo que IdRol representa el rol
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Configurar las propiedades de autenticación
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantener la sesión activa
            };

            // Iniciar sesión
            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

        public async Task<int> ObtenerIdUsuario()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return await Task.FromResult(0);
            }

            return await Task.FromResult(int.Parse(userIdClaim.Value));
        }

        public async Task<bool> PerteneceRol(int userId, string rolName)
        {
            // Verificar si el usuario pertenece al rol especificado
            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == userId);

            if (usuario == null || usuario.IdRolNavigation.Rol != rolName)
                return false;

            return true;
        }
    }
}
