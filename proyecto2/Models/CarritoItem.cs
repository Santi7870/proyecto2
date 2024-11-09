using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto2.Models
{
    [Table("CarritoItems")]
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Usuario { get; set; }  // Nueva propiedad para el nombre del usuario
    }
}

