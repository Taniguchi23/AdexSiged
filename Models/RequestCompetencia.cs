using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using SIGED_API.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Models
{
    public class RequestCompetencia
    {
        [Key]

        [NotMapped]
        public int e_competencia_id { get; set; }
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
        public string comentario_12 { get; set; }
        public string comentario_13 { get; set; }
        public string comentario_14 { get; set; }
        public string comentario_15 { get; set; }
        public string comentario_16 { get; set; }
        public string comentario_17 { get; set; }
        public string comentario_18 { get; set; }
        public string comentario_19 { get; set; }
        public string comentario_20 { get; set; }
        public bool estado { get; set; }

        [NotMapped]
        public int sel_detalle_id { get; set; }
        public int seleccion_id { get; set; }
        public int evaluacion_id { get; set; }

        public List<E_Habilidad_Competencia> E_Habilidad_Competencias { get; set; }

        public class E_Habilidad_Competencia
        {
            public int id_detalle_e_h_competencia { get; set; }
            public int e_competencia_i { get; set; }
            public int id_habilidad { get; set; }
            public int id_valoracion { get; set; }
        }
    }
}
