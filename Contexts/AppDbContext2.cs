using Microsoft.EntityFrameworkCore;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo = SIGED_API.Models.Modelo;
using Postulante = SIGED_API.Entity.Postulante;
using REPORTE = SIGED_API.Models.REPORTE;

namespace SIGED_API.Contexts
{
    public class AppDbContext2:DbContext
    {

        public AppDbContext2(DbContextOptions<AppDbContext2> options):base(options)
        {
        }

        public DbSet<Postulante> Postulante { get; set; }

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

        public DbSet<PAGO> PAGO { get; set; }


        public DbSet<REPORTE> REPORTE { get; set; }

        public DbSet<RUTINA1> RUTINA1 { get; set; }

        public DbSet<RUTINA2> RUTINA2 { get; set; }

        public DbSet<RUTINA3> RUTINA3 { get; set; }

        public DbSet<RUTINA4> RUTINA4 { get; set; }


        public DbSet<EVALUACION> EVALUACION { get; set; }

        public DbSet<DETALLE_EVALUACION> DETALLE_EVALUACION { get; set; }

        public DbSet<NOTIFICACION> NOTIFICACION { get; set; }

        public DbSet<PROGRAMACION> PROGRAMACION { get; set; }


        public DbSet<TEMPORAL_IMAGEN> TEMPORAL_IMAGEN { get; set; }

        public DbSet<ENVIAR_CORREO> ENVIAR_CORREO { get; set; }
    }
}
