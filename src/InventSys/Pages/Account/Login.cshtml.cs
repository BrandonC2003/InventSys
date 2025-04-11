using InventSys.Application.DTOs;
using InventSys.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventSys.Pages.Account
{
    public class LoginModel(UsuarioUseCase usuarioUseCase) : PageModel
    {

        private readonly UsuarioUseCase _usuarioUseCase = usuarioUseCase;

        [BindProperty]
        public LogInDto Input { get; set; }

        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
                return Page();


            if (!await _usuarioUseCase.IniciarSesionAsync(Input))
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return Page();
            }

            return LocalRedirect(ReturnUrl);
        }
    }
}
