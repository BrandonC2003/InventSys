using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace InventSys.Infrastructure.Services
{
    public class AuthService(InventSysDbContext context, IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly InventSysDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<bool> CerrarSesionAsync(int userId)
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

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

            var claimsIdentity = new ClaimsIdentity(claims, "CustomAuth");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authProperties);

            return true;
        }
    }
}
