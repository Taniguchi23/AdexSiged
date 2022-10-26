using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class DETALLE_EVALUACION
    {
        [Key]
        public int DETALLE_EVALUACION_ID { get; set; }

        public int EVALUACION_ID { get; set; }

        public int COMPONENTE_ID { get; set; }

        public int CALIFICACION { get; set; }

        public int PUNTAJE { get; set; }
    }
}
