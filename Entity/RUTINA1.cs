using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class RUTINA1
    {
        [Key]
        public int RUTINA1_ID { get; set; }

        public int REPORTE_ID { get; set; }

        public int RUTINA_ID { get; set; }

        public decimal CALIFICACION { get; set; }
    }
}
