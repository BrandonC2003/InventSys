using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace InventSys.Components.Auth
{
    public class CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext != null) // Ensure HttpContext is not null
                {
                    var authResult = await httpContext.AuthenticateAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    if (authResult?.Principal != null)
                    {
                        principal = authResult.Principal;
                    }
                }
            }
            catch
            {
                // lo tomará como no autenticado
            }

            return new AuthenticationState(principal);
        }
    }
}
