using Microsoft.EntityFrameworkCore;
using SIGED_API.Entity;
//using SIGED_API.Entity;
//using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postulante = SIGED_API.Ficha.Postulante;

namespace SIGED_API.Contexts
{
    public class AppDbContext3:DbContext
    {

        public AppDbContext3(DbContextOptions<AppDbContext3> options):base(options)
        {
        }

        public DbSet<Postulante> Postulante { get; set; }
        public DbSet<Estudio_Realizado> Estudio_Realizado { get; set; }


        public DbSet<Pregrado> Pregrado { get; set; }

        public DbSet<Postgrado> Postgrado { get; set; }


        public DbSet<NIVEL_INGLES> NIVEL_INGLES { get; set; }

        public DbSet<NIVEL_OFIMATICA> NIVEL_OFIMATICA { get; set; }

        public DbSet<EXPERIENCIA> EXPERIENCIA { get; set; }

        public DbSet<EXPERIENCIA_LABORAL> EXPERIENCIA_LABORAL { get; set; }


        public DbSet<COMPOSICION_FAMILIAR> COMPOSICION_FAMILIAR { get; set; }

        public DbSet<COMPOSICION_HIJO> COMPOSICION_HIJO { get; set; }

        public DbSet<PAGO> PAGO { get; set; }

        public DbSet<DECLARACION_JURADA> DECLARACION_JURADA { get; set; }

        public DbSet<TEMPORAL_IMAGEN> TEMPORAL_IMAGEN { get; set; }

    }
}
