using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zoologico_Modelos;

    public class WebZOOAPIContext : DbContext
    {
        public WebZOOAPIContext (DbContextOptions<WebZOOAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Zoologico_Modelos.Raza> Razas { get; set; } = default!;

public DbSet<Zoologico_Modelos.Especie> Especies { get; set; } = default!;

public DbSet<Zoologico_Modelos.Animal> Animales { get; set; } = default!;
    public object Raza { get; internal set; }
}
