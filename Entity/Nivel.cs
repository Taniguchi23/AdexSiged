using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Nivel
    {
        [Key]
        public int id_valoracion { get; set; }
        public string nivel { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
