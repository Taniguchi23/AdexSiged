using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class TIPO_DOCUMENTO
    {
        [Key]
        public int tipo_id { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
