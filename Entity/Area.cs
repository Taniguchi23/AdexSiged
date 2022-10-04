using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Area
    {
        [Key]
        public int area_id { get; set; }
        public string area { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
