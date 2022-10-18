using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Models
{
    public class EvaluacionTecnica_Request
    {
        [Key]
        public int e_tecnica_id { get; set; }
        public DateTime fecha { get; set; }
        public string comentario_1 { get; set; }
        public string comentario_2 { get; set; }
        public string comentario_3 { get; set; }
        public string comentario_4 { get; set; }
        public string comentario_5 { get; set; }
        public string comentario_6 { get; set; }
        public string comentario_7 { get; set; }
        public string comentario_8 { get; set; }
        public string comentario_9 { get; set; }
        public string comentario_10 { get; set; }
        public string comentario_11 { get; set; }
        public string apreciacion { get; set; }

        public int id_hora_pedagogica { get; set; }

        public string observacion { get; set; }
        public bool estado { get; set; }
        public int sel_detalle_id { get; set; }
        public int seleccion_id { get; set; }
        public int evaluacion_id { get; set; }
    }
}
