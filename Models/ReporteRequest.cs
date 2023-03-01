using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SIGED_API.Models
{
    public class ReporteRequest
    {
        [Key]
        public int reporte_id { get; set; }

        public int postulante_id { get; set; }

        public int evaluador_id { get; set; }

        public DateTime fecha { get; set; }

        public int cal_rutina1 { get; set; }

        public int cal_rutina2 { get; set; }

        public int cal_rutina3 { get; set; }

        public int cal_rutina4 { get; set; }

        public int nota_final { get; set; }

        public string observaciones { get; set; }

        public string archivo { get; set; }

        public string estado { get; set; }

        public List<rutina1> rutinas1 { get; set; }
        public class rutina1
        {
            public int rutina1_id { get; set; }

            public int reporte_id { get; set; }

            public int rutina_id { get; set; }

            public decimal calificacion { get; set; }
        }

        public List<rutina2> rutinas2 { get; set; }
        public class rutina2
        {
            public int rutina1_id { get; set; }

            public int reporte_id { get; set; }

            public int rutina_id { get; set; }

            public decimal calificacion { get; set; }
        }


        public List<rutina3> rutinas3 { get; set; }
        public class rutina3
        {
            public int rutina1_id { get; set; }

            public int reporte_id { get; set; }

            public int rutina_id { get; set; }

            public decimal calificacion { get; set; }
        }


        public List<rutina4> rutinas4 { get; set; }
        public class rutina4
        {
            public int rutina1_id { get; set; }

            public int reporte_id { get; set; }

            public int rutina_id { get; set; }

            public decimal calificacion { get; set; }
        }
    }
}
