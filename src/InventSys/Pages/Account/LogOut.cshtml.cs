using InventSys.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventSys.Pages.Account
{
    public class LogOutModel(UsuarioUseCase usuarioUseCase) : PageModel
    {
        private readonly UsuarioUseCase _usuarioUseCase = usuarioUseCase;
        public async Task<IActionResult> OnGet()
        {
            await _usuarioUseCase.CerrarSesionAsync();
            return LocalRedirect("/Account/Login");
        }
    }
}
