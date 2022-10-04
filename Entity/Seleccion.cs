using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Seleccion
    {
        [Key]
        public int seleccion_id { get; set; }
        public DateTime fecha_sel { get; set; }
        public int postulante_id { get; set; }
        public int evaluador_id { get; set; }
        public int semestre_id { get; set; }
        public int area_id { get; set; }
    }
}
