using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Models.Request
{
    public class Postulante
    {
        [Key]
        public int postulante_id { get; set; }
        public string estado { get; set; }
        public bool estado_contratado { get; set; }
    }
}
