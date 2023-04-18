using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class PROGRAMACION
    {
        [Key]
        public int? PROGRAMACION_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public DateTime FECHA { get; set; }
        public DateTime CREATED_AT { get; set; }
        public string? ESTADO { get; set; }

    }
}
