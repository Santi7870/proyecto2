using Microsoft.EntityFrameworkCore;
using proyecto2.Models;

namespace proyecto2.Data
{
    public class Proyecto2Context : DbContext
    {
        public Proyecto2Context(DbContextOptions<Proyecto2Context> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Compra> Compras { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarritoItem>()
                .Property(c => c.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Compra>()
                .Property(c => c.Total)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }


    }
}



