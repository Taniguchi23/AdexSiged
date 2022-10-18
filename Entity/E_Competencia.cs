using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class E_Competencia
    {
        [Key]
      
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
    }
}
