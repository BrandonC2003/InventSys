using InventSysNotificationService;
using Microsoft.EntityFrameworkCore;
using InventSys.Infrastructure.Data.EntityFramework;
using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Services;
using InventSys.Domain.Entities;
using Serilog;



var builder = Host.CreateApplicationBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Reemplazar el sistema de logging por defecto con Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();


builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<InventSysDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventSysDb")));

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.Configure<ProductAlertSettings>(builder.Configuration.GetSection("ProductAlertSettings"));

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddTransient<ITrazaAlertaService, TrazaAlertaService>();
builder.Services.AddTransient<INotificationService, EmailService>();
builder.Services.AddTransient<IRolCatalogoService, RolCatalogoService>();

var host = builder.Build();
host.Run();
