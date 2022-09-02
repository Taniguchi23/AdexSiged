using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Postulante
    {
        [Key]
        public int POSTULANTE_ID { get; set; }
        public string NOMBRE { get; set; }
        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }

    }
}
