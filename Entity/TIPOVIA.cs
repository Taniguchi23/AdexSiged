using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class TIPOVIA
    {
        [Key]
        public int TIPOVIA_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }
    }
}
