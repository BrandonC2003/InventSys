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
        public required LogInDto Input { get; set; }
        public string? ErrorMessage { get; set; }

        public string? ReturnUrl { get; set; }

        public IActionResult OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            ErrorMessage = string.Empty;

            if (!ModelState.IsValid)
                return Page();


            try
            {
                if (!await _usuarioUseCase.IniciarSesionAsync(Input))
                {
                    ErrorMessage = "Las credenciales son incorrectas";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
            

            return LocalRedirect(ReturnUrl);
        }
    }
}
