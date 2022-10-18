using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class E_Habilidad_Competencia
    {
        [Key]
        public int id_detalle_e_h_competencia { get; set; }
        public int e_competencia_i { get; set; }
        public int id_habilidad { get; set; }
        public int id_valoracion { get; set; }

    }
}
