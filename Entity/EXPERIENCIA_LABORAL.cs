using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class EXPERIENCIA_LABORAL
    {

        [Key]
        public int EXPERIENCIA_LABORAL_ID { get; set; }

        public int EXPERIENCIA_ID { get; set; }

        public string EMPRESA { get; set; }

        public string CARGO { get; set; }

        public string JEFE_INMEDIATO { get; set; }

        public string TELEFONO { get; set; }

        public DateTime FECHA_INGRESO { get; set; }

        public DateTime FECHA_CESE { get; set; }

        public string MOTIVO_CESE { get; set; }

    }
}
