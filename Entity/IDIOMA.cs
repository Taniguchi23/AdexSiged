using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class IDIOMA
    {
        [Key]
        public int IDIOMA_ID { get; set; }

        public int DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }
    }
}
