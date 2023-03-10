using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class EVALUACION
    {
        [Key]
        public int evaluacion_id { get; set; }

        public int administrador_id { get; set; }

        public int especialidad_id { get; set; }

        public DateTime fecha { get; set; }

        public bool estado { get; set; }

     



    }
}
