using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Parametro
    {
        [Key]
        public int parametro_id { get; set; }
        public string descripcion { get; set; }
        public int id_padre { get; set; }
    }
}
