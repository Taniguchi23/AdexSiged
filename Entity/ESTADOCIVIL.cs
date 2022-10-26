using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class ESTADOCIVIL
    {
        [Key]
        public int estadocivil_id { get; set; }

        public string descripcion { get; set; }

        public bool estado { get; set; }

    }
}
