using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class NOTIFICACION
    {

        [Key]
        //public int notificacion_id { get; set; }

        public int postulante_id { get; set; }

        public DateTime fecha_actividad { get; set; }
        public DateTime fecha_desde { get; set; }
        public DateTime fecha_hasta { get; set; }

        public DateTime fecha_programacion { get; set; }


    }
}
