using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class AFP
    {
        [Key]
        public int AFP_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }
    }
}
