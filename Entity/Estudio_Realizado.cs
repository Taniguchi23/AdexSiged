using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Estudio_Realizado
    {
        [Key]
        public int estudio_id { get; set; }

        public int postulante_id { get; set; }

        public string otros { get; set; }

        public string otros_programas { get; set; }

    }
}
