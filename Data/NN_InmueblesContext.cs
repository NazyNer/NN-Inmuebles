using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NN_Inmuebles.Models;

    public class NN_InmueblesContext : DbContext
    {
        public NN_InmueblesContext (DbContextOptions<NN_InmueblesContext> options)
            : base(options)
        {
        }

        public DbSet<NN_Inmuebles.Models.Casa> Casa { get; set; } = default!;

        public DbSet<NN_Inmuebles.Models.Cliente>? Cliente { get; set; }

        public DbSet<NN_Inmuebles.Models.Alquiler>? Alquiler { get; set; }

        public DbSet<NN_Inmuebles.Models.Devolucion>? Devolucion { get; set; }
    }
