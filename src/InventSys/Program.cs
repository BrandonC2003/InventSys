using InventSys.Infrastructure.Services;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using InventSys.Domain.Interfaces;
using InventSys.Components;
using InventSys.Application.UseCases;

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

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar AuthService
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<UsuarioUseCase>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication(); // Habilitar autenticación
app.UseAuthorization();  // Habilitar autorización

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
