using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto2.Models;

namespace proyecto2.Data
{
    public class proyecto2Context : DbContext
    {
        public proyecto2Context (DbContextOptions<proyecto2Context> options)
            : base(options)
        {
        }

        public DbSet<proyecto2.Models.CarritoItem> CarritoItem { get; set; } = default!;
    }
}
