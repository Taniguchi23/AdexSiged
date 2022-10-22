using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class GRADOACAD_PREGRADO
    {
        [Key]
        public int GRADOACA_PRE_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }
    }
}
