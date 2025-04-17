using InventSys.Domain.Entities;

namespace InventSys.Application.DTOs
{
    public class RegistrarAuditoriaDto
    {
        public int IdAuditoriaProducto { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaAccion { get; set; } = DateTime.Now;

        public byte Accion { get; set; }

        public Producto DatosAnteriores { get; set; } = null!;

        public Producto DatosNuevos { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
