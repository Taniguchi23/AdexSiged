using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class DECLARACION_JURADA
    {
        [Key]
        public int declaracion_jurada_id { get; set; }

        public int postulante_id { get; set; }

        public DateTime fecha { get; set; }

        public string firma { get; set; }
    }
}
