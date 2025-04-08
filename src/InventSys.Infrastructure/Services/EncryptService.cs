using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InventSys.Domain.Interfaces;

namespace InventSys.Infrastructure.Services
{
    public class EncryptService : IEncryptService
    {
        private readonly RSA _rsa;

        public EncryptService()
        {
            // Inicializar el proveedor RSA
            _rsa = RSA.Create();
        }

        public async Task<string> EncryptAsync(string plainText)
        {
            // Convertir el texto plano a bytes
            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Encriptar los datos usando la clave pública
            var encryptedBytes = _rsa.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);

            // Convertir los datos encriptados a una cadena Base64
            return await Task.FromResult(Convert.ToBase64String(encryptedBytes));
        }
    }
}
