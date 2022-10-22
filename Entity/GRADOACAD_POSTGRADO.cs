using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class GRADOACAD_POSTGRADO
    {
        [Key]
        public int GRADOACA_POS_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }
    }
}
