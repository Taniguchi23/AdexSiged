using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Habilidad
    {
        [Key]
        public int id_habilidad { get; set; }
        public string habilidad { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
