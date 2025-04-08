using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventSys.Domain.Interfaces
{
    public interface IEncryptService
    {
        Task<string> EncryptAsync(string plainText);
    }
}
