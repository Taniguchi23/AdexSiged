using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class OFIMATICA
    {
        [Key]
        public int OFIMATICA_ID { get; set; }

        public int DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }
    }
}
