using proyecto2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto2.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CompraId")]

        public int CompraId { get; set; }
        public string Usuario2 { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }

        // Relación con CarritoItems
        public List<CarritoItem> CarritoItems { get; set; }  // Relación uno a muchos
    }

}

