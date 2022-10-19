using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class Postgrado
    {
        [Key]
        public int postgrado_id { get; set; }

        public int estudio_id { get; set; }
        public string centro_estudio { get; set; }
        public string especializacion { get; set; }
        public int nivel { get; set; }
        public DateTime fecha_ingreso { get; set; }

        public DateTime fecha_salida { get; set; }

    }
}
