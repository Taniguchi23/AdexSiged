using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class Fortaleza
    {
        [Key]
        public int fortaleza_id { get; set; }

        public int modelo_id { get; set; }

        public string descripcion { get; set; }

    }
}
