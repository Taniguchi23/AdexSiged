using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class E_JefeAcademico
    {
        [Key]
        public int entrevistaja_id { get; set; }
        public DateTime fecha { get; set; }
    
        public string apreciacion { get; set; }

        public int id_hora_pedagogica { get; set; }

        public int id_cargo { get; set; }

        public string observacion { get; set; }
        public bool estado { get; set; }
    }
}
