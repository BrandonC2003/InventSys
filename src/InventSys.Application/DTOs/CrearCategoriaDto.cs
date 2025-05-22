using System.ComponentModel.DataAnnotations;

namespace InventSys.Application.DTOs
{
    public class CrearCategoriaDto
    {
        [Required(ErrorMessage = "Ingresa el nombre de la categoría.")]
        public string NombreCategoria { get; set; }
        public string? Descripcion { get; set; }
    }
}
