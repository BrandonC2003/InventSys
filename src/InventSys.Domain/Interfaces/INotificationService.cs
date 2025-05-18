namespace InventSys.Domain.Interfaces
{
    public interface INotificationService
    {
        public Task EnviarNotificacion(string destinatario, string contenido);
    }
}
