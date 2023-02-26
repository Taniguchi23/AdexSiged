using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Models
{
    public class DetalleEvaluacion
    {
        [Key]
        public string nombre_completo { get; set; }

        public int postulante_id { get; set; }


    }
}
