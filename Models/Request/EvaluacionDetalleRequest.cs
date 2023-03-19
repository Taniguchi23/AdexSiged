using System;
using System.Collections.Generic;

namespace SIGED_API.Models.Request
{
    public class EvaluacionDetalleRequest
    {
        public int evaluador_id { get; set; }
        public string flagTipo { get; set; }
        public int semestre_id { get; set; }
        public DateTime fecha { get; set; }
        public List<detalleEvaluacion> detalle_evaluacion { get; set; }
    }

    public class detalleEvaluacion
    {
        public int postulante_id { get; set; }
        public int? enc_estu { get; set; }
        public int? cum_adm { get; set; }
        public int? cap_doc { get; set; }
        public int? acom_doc { get; set; }
        public int? cum_vir { get; set; }
        public Decimal? nota_final { get; set; }
        
    }
}
