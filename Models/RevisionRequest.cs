using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Models
{
    public class RevisionRequest
    {
        [Key]
        public int revision_id { get; set; }
        public DateTime fecha_rev { get; set; }
        public bool seleccion_c1 { get; set; }
        public bool seleccion_c2 { get; set; }
        public bool seleccion_c3 { get; set; }
        public bool seleccion_c4 { get; set; }
        public bool seleccion_c5 { get; set; }
        public bool seleccion_c6 { get; set; }
        public string comentario_1 { get; set; }
        public string comentario_2 { get; set; }
        public string comentario_3 { get; set; }
        public string comentario_4 { get; set; }
        public string comentario_5 { get; set; }
        public string comentario_6 { get; set; }

        public string observacion { get; set; }

        public bool estado { get; set; }

        public int sel_detalle_id { get; set; }
        public int seleccion_id { get; set; }
        public int evaluacion_id { get; set; }
    }
}
