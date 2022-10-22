using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class NIVEL_INGLES
    {

        [Key]
        public int NIVEL_INGLES_ID { get; set; }

        public int ESTUDIO_ID { get; set; }

        public int IDIOMA_ID { get; set; }

        public int NIVELESTUDIO_ID { get; set; }
    }
}
