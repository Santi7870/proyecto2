using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto2.Models
{
    public class Compra
    {

        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }  // Nombre del usuario

        public string NombreProducto { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PrecioTotal { get; set; }  // Precio de la compra
        public DateTime Fecha { get; set; }  // Asegúrate de que no sea nullable si no quieres permitir nulos
    }

}




