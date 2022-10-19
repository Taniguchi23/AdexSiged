using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Pregrado
    {
        [Key]
        public int pregrado_id { get; set; }

        public int estudio_id { get; set; }
        public string centro_estudio { get; set; }
        public string carrera { get; set; }
        public int grado_acad { get; set; }
        public DateTime fecha_ingreso { get; set; }

        public DateTime fecha_salida { get; set; }

   
    }
}
