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

        public int administrador_id { get; set; }

        public int especialidad_id { get; set; }

        public DateTime fecha { get; set; }

        //public bool modulo_id { get; set; }

        //public int calificacion { get; set; }

        public List<DETALLE_EVALUACION> Detalle_evaluacion { get; set; }
        public class DETALLE_EVALUACION
        {
            public int detalle_evaluacion_id { get; set; }
            public int evaluacion_id { get; set; }
            public int postulante_id { get; set; }
            public int enc_estu { get; set; }
            public int cum_adm { get; set; }
            public int cap_doc { get; set; }
            public int acom_doc { get; set; }
            public int cum_vir { get; set; }
            public decimal nota_final { get; set; }

        }
    }
}
