using InventSys.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace InventSys.Application.DTOs
{
    public class ProductoDto
    {
        [Required(ErrorMessage = "Ingresa el nombre del producto")]
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Selecciona una categoria")]
        public Categoria Categoria { get; set; }
        [Required(ErrorMessage = "Selecciona el proveedor")]
        public Proveedor Proveedor { get; set; }
        [Required(ErrorMessage = "Ingresa el precio de compra")]
        [Range(0.0, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor o igual a cero")]
        public double PrecioCompra { get; set; }
        [Required(ErrorMessage = "Ingresa el precio de venta")]
        [Range(0.0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a cero")]
        public double PrecioVenta { get; set; }
        [Required(ErrorMessage = "Ingresa el stok")]
        [Range(0, int.MaxValue, ErrorMessage = "El stok debe ser mayor o igual a cero")]
        public int Stok { get; set; }
        [Required(ErrorMessage = "Ingresa el stok bajo")]
        [Range(0, int.MaxValue, ErrorMessage = "El stok bajo debe ser mayor o igual a cero")]
        public int StokBajo { get; set; }
        [Required(ErrorMessage = "Ingresa el mensaje de alerta")]
        public string MensajeAlerta { get; set; }
        
        public string RazonCambio { get; set; }
    }
}
