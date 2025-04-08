using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventSys.Application.DTOs
{
    public class UsuarioDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string NuevoPassword { get; set; }
        public required string NuevoPassword2 { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public bool Activo { get; set; } = true;
    }
}
