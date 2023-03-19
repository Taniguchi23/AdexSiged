using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Models
{
    public class DetalleEvaluacion
    {
        [Key]
        public string nombre_completo { get; set; }

        public int postulante_id { get; set; }

        public int evaluador_id { get; set; }

        public int detalle_evaluacion_id { get; set; }

        public int? enc_estu  { get; set; }

        public int? cum_adm  { get; set; }

        public int? cap_doc { get; set; }

        public int? acom_doc { get; set; }

        public int? cum_vir  { get; set; }

        public decimal?   nota_final { get; set; }
        public DateTime? fecha_guardado { get; set; }
        public DateTime? fecha_registro { get; set; }
        public Char estado { get; set; }
        public int semestre_id { get;}
    }
}
