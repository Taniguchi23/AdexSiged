using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class EVALUACION
    {
        [Key]
        public int EVALUACION_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public int COORDINADOR_ID { get; set; }

        public int MODULO_ID { get; set; }

        public DateTime FECHA { get; set; }

        public int CALIFICACION { get; set; }

    }
}
