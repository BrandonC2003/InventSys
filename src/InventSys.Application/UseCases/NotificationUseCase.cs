using InventSys.Domain.Interfaces;

namespace InventSys.Application.UseCases
{
    public class NotificationUseCase(INotificationService notificationService)
    {
        private readonly INotificationService _notificationService = notificationService;

        /// <summary>
        /// Envía las notificaciones por todos los canales disponibles.
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="contenido"></param>
        /// <returns></returns>
        private async Task EnviarNotificaciones(string destinatario, string contenido)
        {
            await _notificationService.EnviarNotificacion(destinatario, contenido);
        }

    }
}
