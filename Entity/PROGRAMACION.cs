using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class PROGRAMACION
    {
        [Key]
        public int programacion_id { get; set; }

        public int postulante_id { get; set; }

        public int fecha { get; set; }

        public bool estado { get; set; }
    }
}
