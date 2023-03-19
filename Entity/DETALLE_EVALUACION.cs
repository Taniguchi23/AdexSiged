using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class DETALLE_EVALUACION
    {
        [Key]
        public int DETALLE_EVALUACION_ID { get; set; }

        public int EVALUADOR_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public int? ENC_ESTU { get; set; }

        public int? CUM_ADM { get; set; }

        public int? CAP_DOC { get; set; }

        public int? ACOM_DOC { get; set; }

        public int? CUM_VIR { get; set; }

        public decimal? NOTA_FINAL { get; set; }
        public DateTime? FECHA_GUARDADO { get; set; }
        public DateTime? FECHA_REGISTRO { get; set; }
        public Char ESTADO { get; set; }

        public int SEMESTRE_ID { get; set; }
    }
}
