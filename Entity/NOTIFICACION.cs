using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class NOTIFICACION
    {

        [Key]
        public int notificacion_id { get; set; }

        public int postulante_id { get; set; }

        public int fecha { get; set; }

        public bool estado { get; set; }

    }
}
