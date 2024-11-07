using proyecto2.Models;
using System.ComponentModel.DataAnnotations;

namespace proyecto2.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }

        // Relación con CarritoItems
        public List<CarritoItem> CarritoItems { get; set; }  // Relación uno a muchos
    }

}

