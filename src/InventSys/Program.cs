using InventSys.Infrastructure.Services;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using InventSys.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<InventSysDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventSysDb")));

// Configurar autenticación basada en cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login"; // Ruta para la página de inicio de sesión
        options.AccessDeniedPath = "/AccessDenied"; // Ruta para acceso denegado
    });

// Registrar AuthService
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddHttpContextAccessor();

// Agregar servicios de Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configurar middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Habilitar autenticación
app.UseAuthorization();  // Habilitar autorización

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
