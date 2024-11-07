using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto2.Models
{
    [Table("CarritoItems")]  // Asegúrate de que el nombre de la tabla coincida
    public class CarritoItem
    {

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
