using System.Security.Cryptography;
using System.Text;
using InventSys.Domain.Interfaces;

namespace InventSys.Infrastructure.Services
{
    public class EncryptService : IEncryptService
    {
        public async Task<string> EncryptAsync(string plainText)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(plainText));
            return await Task.FromResult(Convert.ToBase64String(hashedBytes));
        }

    }
}
