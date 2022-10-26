using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class BANCO
    {
        [Key]
        public int banco_id { get; set; }

        public string descripcion { get; set; }

        public bool estado { get; set; }
    }
}
