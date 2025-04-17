using InventSys.Application.UseCases;
using InventSys.Components.Auth;
using InventSys.Components;
using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Data.EntityFramework;
using InventSys.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudBlazor;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<InventSysDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventSysDb")));

// Configuración de autenticación (DEBE ir antes de AddRazorComponents)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyAppAuthCookie";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();

// Servicios adicionales
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<IRolCatalogoService, RolCatalogoService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IAuditoriaProductoService, AuditoriaProductoService>();
builder.Services.AddScoped<UsuarioUseCase>();
builder.Services.AddScoped<ProveedorUseCase>();
builder.Services.AddScoped<CategoriaUseCase>();
builder.Services.AddScoped<RolCatalogoUseCase>();
builder.Services.AddScoped<AuditoriaProductoUseCase>();

// Configuración de Blazor y Razor Pages
builder.Services.AddRazorPages(); 
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = true;
});

var app = builder.Build();

// Configuración del pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ORDEN CRÍTICO DE MIDDLEWARES:
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Configuración de endpoints
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();