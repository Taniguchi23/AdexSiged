using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Especialidad_postulante
    {
        [Key]
        public int especialidad_post_id { get; set; }
        public int especialidad_id { get; set; }
        public int postulante_id { get; set; }
    }
}
