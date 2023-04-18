using System;
using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Models
{
    public class DetalleProgramacion
    {
        [Key]
        public string nombre_completo { get; set; }

        public int postulante_id { get; set; }

        public string estado { get; set; }
        public DateTime? fecha { get; set; }
        public DateTime? created_at { get; set; }
        
    }
}
