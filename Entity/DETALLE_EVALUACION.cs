using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class DETALLE_EVALUACION
    {
        [Key]
        public int DETALLE_EVALUACION_ID { get; set; }

        public int EVALUACION_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public int ENC_ESTU { get; set; }

        public int CUM_ADM { get; set; }

        public int CAP_DOC { get; set; }

        public int ACOM_DOC { get; set; }

        public int CUM_VIR { get; set; }

        public decimal NOTA_FINAL { get; set; }
    }
}
