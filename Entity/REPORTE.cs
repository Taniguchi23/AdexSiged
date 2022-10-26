using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class REPORTE
    {

        [Key]
        public int reporte_id { get; set; }

        public int postulante_id { get; set; }

        public int evaluador_id { get; set; }

        public int area_id { get; set; }

        public DateTime fecha { get; set; }

    }
}
