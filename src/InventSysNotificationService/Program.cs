using InventSysNotificationService;
using Microsoft.EntityFrameworkCore;
using InventSys.Infrastructure.Data.EntityFramework;
using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Services;
using InventSys.Application.UseCases;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<InventSysDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventSysDb")));

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddTransient<ITrazaAlertaService, TrazaAlertaService>();

builder.Services.AddTransient<UsuarioUseCase>();
builder.Services.AddTransient<ProductoUseCase>();
builder.Services.AddTransient<TrazaAlertaUseCase>();

var host = builder.Build();
host.Run();
