using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using SIGED_API.Entity;

namespace SIGED_API.Models
{
    public class EvaluacionRequest
    {
        [Key]
        public int evaluacion_id { get; set; }

        public int postulante_id { get; set; }

        public int coordinador_id { get; set; }

        public int modulo_id { get; set; }

        public DateTime fecha { get; set; }

        public int calificacion { get; set; }

        public List<DETALLE_EVALUACION> Detalle_evaluacion { get; set; }
        public class DETALLE_EVALUACION
        {
            public int detalle_evaluacion_id { get; set; }
            public int evaluacion_id { get; set; }
            public int componente_id { get; set; }
            public int calificacion { get; set; }
            public int puntaje { get; set; }

        }
    }
}
