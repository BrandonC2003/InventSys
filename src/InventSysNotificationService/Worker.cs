using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;

namespace InventSysNotificationService
{
    public class Worker(
        ILogger<Worker> logger, IServiceProvider serviceProvider, IOptions<ProductAlertSettings> productAlertSettings) : BackgroundService
    {
        private readonly ILogger<Worker> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ProductAlertSettings _productAlertSettings = productAlertSettings.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    // Resolver servicios dentro del scope
                    var productoService = scope.ServiceProvider
                        .GetRequiredService<IProductoService>();

                    var trazaAlertaService = scope.ServiceProvider
                        .GetRequiredService<ITrazaAlertaService>();

                    var notificationService = scope.ServiceProvider
                        .GetRequiredService<INotificationService>();

                    var usuarioService = scope.ServiceProvider
                        .GetRequiredService<IUsuarioService>();

                    var rolCatalogoService = scope.ServiceProvider
                        .GetRequiredService<IRolCatalogoService>();

                    var productosStokBajo = await ExcluirProductosAlertasRecientes(await productoService.ObtenerProductosConStokBajo(),productoService);

                    if (productosStokBajo != null && productosStokBajo.Count > 0)
                    {
                        var usuariosDestinatarios = await ObtenerUsuariosDestinatarios(usuarioService, rolCatalogoService);

                        foreach (var product in productosStokBajo)
                        {
                            await EnviarAlertas(usuariosDestinatarios, product, trazaAlertaService, notificationService);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No hay productos con stock bajo");
                    }

                    await Task.Delay(_productAlertSettings.CheckIntervalSeconds * 1000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error en ExecuteAsync: {message} {inner}", ex.Message, ex.InnerException?.Message);
                }
            }
        }

        private async Task<List<Usuarios>> ObtenerUsuariosDestinatarios(
            IUsuarioService usuarioService,
            IRolCatalogoService rolCatalogoService)
        {
            var roles = await rolCatalogoService.ObtenerRolesAsync();
            var rolesPermitidos = roles
                .Where(r => r.Rol == "Administrador" || r.Rol == "Almacenista")
                .Select(r => r.IdRol)
                .ToList();

            var usuarios = await usuarioService.ObtenerUsuariosAsync();
            return usuarios
                .Where(u => rolesPermitidos.Contains(u.IdRol))
                .ToList();
        }
        
        /// <summary>
        /// Excluye de la lista de productos, los productos que hayan sido alertados durante la última hora
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="trazaAlertaService"></param>
        /// <returns></returns>
        private async Task<List<Producto>> ExcluirProductosAlertasRecientes(List<Producto> productos, IProductoService productoService)
        {
            var productosSinAlertasRecientes = new List<Producto>();

            foreach (var producto in productos) 
            {
                if (! await productoService.TieneAlertas(producto.IdProducto, _productAlertSettings.AlertResendIntervalMinutes))
                {
                    productosSinAlertasRecientes.Add(producto);
                }
            }

            return productosSinAlertasRecientes;
        }

        private async Task EnviarAlertas(
            List<Usuarios> destinatarios,
            Producto producto,
            ITrazaAlertaService trazaAlertaService,
            INotificationService notificationService)
        {
            if (destinatarios == null || destinatarios.Count == 0)
            {
                _logger.LogInformation("No hay destinatarios para enviar la alerta");
                return;
            }

            foreach (var destinatario in destinatarios)
            {
                try
                {
                    var nuevaAlerta = new TrazaAlerta
                    {
                        IdUsuario = destinatario.IdUsuario,
                        IdProducto = producto.IdProducto,
                        EstadoAlerta = (byte)EstadoAlerta.Creado,
                        Fecha = DateTime.Now,
                        Contenido = producto.MensajeAlerta
                    };

                    var trazaAlerta = await trazaAlertaService.GuardarTrazaAlertaAsync(nuevaAlerta);
                    await notificationService.EnviarNotificacion(destinatario.CorreoElectronico, producto.MensajeAlerta);
                    await trazaAlertaService.ActualizarEstadoAlertaAsync(trazaAlerta.IdTrazaAlerta, EstadoAlerta.Enviada);

                    _logger.LogInformation("Alerta de stock bajo del producto {producto} enviada al correo {correo}",
                        producto.NombreProducto, destinatario.CorreoElectronico);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al enviar la alerta del producto {producto} al correo {correo}: {message} {inner}",
                        producto.NombreProducto, destinatario.CorreoElectronico, ex.Message, ex.InnerException?.Message);
                }
            }
        }
    }
}
