using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto2.Models
{
    public class Compra
    {

        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }  // Nombre del usuario

        [Column(TypeName = "decimal(18,2)")]

        public decimal PrecioTotal { get; set; }  // Precio de la compra
        public DateTime FechaCompra { get; set; } = DateTime.Now;  // Fecha de la compra
    }

}




