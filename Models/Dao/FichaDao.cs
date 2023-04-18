using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using Postulant = SIGED_API.Entity.Postulante;

namespace SIGED_API.Models.Dao
{
    public class FichaDao
    {
        public int Postulanteid { get; set; }
        public Postulant Postulante { get; set; }
        public List<Pregrado>? Pregrados { get; set; }
        public List<Postgrado>? Postgrados { get; set; }
        public List<NIVEL_INGLES>? NivelIngles { get; set; }
        public List<NIVEL_OFIMATICA>? NivelOfimatica { get; set; }
        public List<EXPERIENCIA_LABORAL>? ExperienciaLaboral { get; set; }
        public COMPOSICION_FAMILIAR? CommposicionFamiliar { get; set; }
        public List<COMPOSICION_HIJO>? ComposicionHijo{ get; set; }
        public PAGO? PAGO { get; set; }
        public DECLARACION_JURADA? DeclaracionJurada { get; set; }
    }

}
