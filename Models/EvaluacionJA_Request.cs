using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Models
{
    public class EvaluacionJA_Request
    {
        [Key]
        public int entrevistaja_id { get; set; }
        public DateTime fecha { get; set; }

        public string apreciacion { get; set; }

        public int id_hora_pedagogica { get; set; }

        public int id_cargo { get; set; }

        public string observacion { get; set; }
        public bool estado { get; set; }

        public int sel_detalle_id { get; set; }
        public int seleccion_id { get; set; }
        public int evaluacion_id { get; set; }
    }
}
