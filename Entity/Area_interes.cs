using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Area_interes
    {
        [Key]
        public int area_id { get; set; }
        public int postulante_id { get; set; }
    }
}
