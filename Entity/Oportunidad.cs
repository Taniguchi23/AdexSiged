using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Oportunidad
    {
        [Key]
        public int oportunidad_id { get; set; }

        public int modelo_id { get; set; }

        public string descripcion { get; set; }
    }
}
