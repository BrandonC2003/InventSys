using InventSys.Application.DTOs;
using InventSys.Application.UseCases;
using InventSys.Domain.Enums;
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

        public async Task<IActionResult> OnGet(string? returnUrl = null)
        {
            var hayUsuariosRegistrados = await _usuarioUseCase.HayUsuariosRegistrado();
            if (!hayUsuariosRegistrados) 
            {
                await _usuarioUseCase.DarAccesoTemporalAsync();
                return LocalRedirect("/crearPrimerUsuario");
            }

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
                int userId = await _usuarioUseCase.IniciarSesionAsync(Input);
                if (userId >= 0)
                {
                    await _usuarioUseCase.CambiarEstadoAsync(userId, UserStatus.Conectado);
                }
                else
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
