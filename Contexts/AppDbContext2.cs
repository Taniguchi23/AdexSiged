using Microsoft.EntityFrameworkCore;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo = SIGED_API.Models.Modelo;

namespace SIGED_API.Contexts
{
    public class AppDbContext2:DbContext
    {

        public AppDbContext2(DbContextOptions<AppDbContext2> options):base(options)
        {
        }

        public DbSet<Rubrica_Modelo> Rubrica_Modelo { get; set; }

        public DbSet<Fortaleza> Fortaleza { get; set; }

        public DbSet<Oportunidad> Oportunidad { get; set; }

        public DbSet<Modelo> Modelo { get; set; }


        public DbSet<Estudio_Realizado> Estudio_Realizado { get; set; }


        public DbSet<Pregrado> Pregrado { get; set; }

        public DbSet<Postgrado> Postgrado { get; set; }


        public DbSet<NIVEL_INGLES> NIVEL_INGLES { get; set; }

        public DbSet<NIVEL_OFIMATICA> NIVEL_OFIMATICA { get; set; }

        public DbSet<EXPERIENCIA> EXPERIENCIA { get; set; }

        public DbSet<EXPERIENCIA_LABORAL> EXPERIENCIA_LABORAL { get; set; }


        public DbSet<COMPOSICION_FAMILIAR> COMPOSICION_FAMILIAR { get; set; }

        public DbSet<COMPOSICION_HIJO> COMPOSICION_HIJO { get; set; }

    }
}
