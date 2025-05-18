using InventSys.Domain.Entities;
using InventSys.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace InventSys.Infrastructure.Services
{
    public class EmailService(IOptions<SmtpSettings> smtpSettings) : INotificationService
    {
        private readonly SmtpSettings _smtpSettings = smtpSettings.Value;
        public async Task EnviarNotificacion(string destinatario, string contenido)
        {
            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.User),
                Subject = "Alerta de stock bajo",
                Body = contenido,
                IsBodyHtml = false
            };
            
            mail.To.Add(destinatario);

            await client.SendMailAsync(mail);
        }
    }
}
