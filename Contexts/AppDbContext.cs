using Microsoft.EntityFrameworkCore;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Contexts
{
    public class AppDbContext:DbContext
    {
        //public AppDbContext()
        //{
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Postulante> Postulante { get; set; }

 

        public DbSet<Area> Area { get; set; }

        public DbSet<Area_interes> Area_interes { get; set; }

        public DbSet<Especialidad> Especialidad { get; set; }

        public DbSet<Especialidad_postulante> Especialidad_postulante { get; set; }

        public DbSet<Parametro> Parametro { get; set; }

        public DbSet<Seleccion> Seleccion_cabecera { get; set; }

        public DbSet<Postulante> Login { get; set; }

        public DbSet<Semestre> Semestre { get; set; }

        public DbSet<Tarifa> Tarifa { get; set; }

        public DbSet<Revision> RevisionCV { get; set; }

        public DbSet<Seleccion_detalle> Seleccion_detalle { get; set; }

        public DbSet<E_Competencia> E_Competencia { get; set; }

        public DbSet<E_Habilidad_Competencia> E_Habilidad_Competencia { get; set; }

        public DbSet<E_Tecnica> E_Tecnica { get; set; }

        public DbSet<E_JefeAcademico> E_JefeAcademico { get; set; }

        public DbSet<Modelo> Modelo { get; set; }

        public DbSet<Nivel> Nivel { get; set; }

        public DbSet<Habilidad> Habilidad { get; set; }

        public DbSet<Rubrica_Modelo> Rubrica_Modelo { get; set; }

        public DbSet<Fortaleza> Fortaleza { get; set; }

        public DbSet<Oportunidad> Oportunidad { get; set; }

        public DbSet<DEPARTAMENTO> DEPARTAMENTO { get; set; }

        public DbSet<PROVINCIA> PROVINCIA { get; set; }

        public DbSet<DISTRITO> DISTRITO { get; set; }

        public DbSet<TIPOVIA> TIPOVIA { get; set; }

        //public DbSet<Pregrado> Pregrado { get; set; }

        //public DbSet<Postgrado> Postgrado { get; set; }

        public DbSet<Rubrica> Rubrica { get; set; }

        public DbSet<GRADOACAD_PREGRADO> GRADOACAD_PREGRADO { get; set; }

        public DbSet<GRADOACAD_POSTGRADO> GRADOACAD_POSTGRADO { get; set; }

        public DbSet<AFP> AFP { get; set; }


        public DbSet<RUTINA> RUTINA { get; set; }

        public DbSet<REPORTE> REPORTE { get; set; }

        public DbSet<ESTADOCIVIL> ESTADOCIVIL { get; set; }

        public DbSet<BANCO> BANCO { get; set; }

        public DbSet<TEMPORAL_IMAGEN> TEMPORAL_IMAGEN { get; set; }

        public DbSet<ROL> ROL { get; set; }

        public DbSet<TIPO_DOCUMENTO> TIPO_DOCUMENTO { get; set; }
        
    }
}
