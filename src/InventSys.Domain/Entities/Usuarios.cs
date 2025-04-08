using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventSys.Domain.Entities
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public byte Estado { get; set; }
        public bool Activo { get; set; }
    }
}
