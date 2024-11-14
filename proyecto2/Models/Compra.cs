using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto2.Models
{
    public class Compra
    {

        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }  

        public string NombreProducto { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PrecioTotal { get; set; } 
        public DateTime Fecha { get; set; }  
    }

}




