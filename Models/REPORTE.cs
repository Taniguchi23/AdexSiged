using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Models
{
    public class REPORTE
    {

        [Key]
        public int REPORTE_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public int EVALUADOR_ID { get; set; }

        public int AREA_ID { get; set; }

        public DateTime FECHA { get; set; }

        public int CAL_RUTINA1 { get; set; }

        public int CAL_RUTINA2 { get; set; }

        public int CAL_RUTINA3 { get; set; }

        public int CAL_RUTINA4 { get; set; }

        public int NOTA_FINAL { get; set; }

        public string OBSERVACIONES { get; set; }

        public string ARCHIVO { get; set; }

        public bool ESTADO { get; set; }
    }
}
