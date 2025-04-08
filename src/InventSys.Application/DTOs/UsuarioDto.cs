using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventSys.Application.DTOs
{
    public class UsuarioDto
    {
        public string UserName { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public string Password { get; set; } = string.Empty;
        public string NuevoPassword { get; set; } = string.Empty;
        public string NuevoPassword2 { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
