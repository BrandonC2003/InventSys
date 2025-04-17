namespace InventSys.Domain.Entities
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
