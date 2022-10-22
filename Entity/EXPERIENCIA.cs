using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class EXPERIENCIA
    {
        [Key]
        public int EXPERIENCIA_ID { get; set; }

        public int POSTULANTE_ID { get; set; }
    }
}
