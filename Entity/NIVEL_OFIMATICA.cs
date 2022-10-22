using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class NIVEL_OFIMATICA
    {
        [Key]
        public int NIVEL_OFIMATICA_ID { get; set; }

        public int ESTUDIO_ID { get; set; }

        public int OFIMATICA_ID { get; set; }

        public int NIVELESTUDIO_ID { get; set; }
    }
}
