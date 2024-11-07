using Microsoft.EntityFrameworkCore;
using proyecto2.Models;
using System.Collections.Generic;

namespace proyecto2.Models
{
    public class Proyecto2Context : DbContext
    {
        // Constructor que recibe opciones de configuración de DbContext
        public Proyecto2Context(DbContextOptions<Proyecto2Context> options)
            : base(options)
        { }

        // DbSet para la entidad Usuario
        public DbSet<Usuario> Usuarios { get; set; }

        // DbSet para la entidad CarritoItem
        public DbSet<CarritoItem> CarritoItems { get; set; }  // Añadido CarritoItems
        public DbSet<Compra> Compras { get; set; }  // Asegúrate de que esté aquí

    }
}


