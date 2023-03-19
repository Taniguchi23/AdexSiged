using SIGED_API.Entity;
using System;
using System.Collections.Generic;

namespace SIGED_API.Models
{
    public class PostulanteDetalle
    {
        public int IdPostulante { get; set; }
        public Boolean FlagSeleccion { get; set; }
        public string NombrePostulante { get; set; }
        public string ApellidoPostulante { get; set; }
        public int? IdSeleccion { get; set; }
        public int? IdSemestre { get; set; }
        public int? IdPreguntaSeleccion { get; set; }
        public List<Semestre> ListaSemestre { get; set; }
        public List<Especialidad> ListaEspecialidadesPostulante { get; set; }
        public List<DetalleEspecialidades> SelectListaEspecialidades { get; set; }
    }

    public class DetalleEspecialidades
    {
        public Especialidad Especialidad { get; set; }
        public List<Especialidad_cursos> Cursos { get; set; }
   
    }
}
