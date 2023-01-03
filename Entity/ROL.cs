using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class ROL
    {
        [Key]
        public int rol_id { get; set; }
        //public int cod_rol   { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; } 

    }
}
