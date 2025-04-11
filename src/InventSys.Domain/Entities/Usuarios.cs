namespace InventSys.Domain.Entities
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public byte Estado { get; set; }
        public bool Activo { get; set; }
    }
}
