using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Tarifa
    {
        [Key]
        public int tarifa_id { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
