using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class RUTINA4
    {
        [Key]
        public int RUTINA4_ID { get; set; }

        public int REPORTE_ID { get; set; }

        public int RUTINA_ID { get; set; }

        public int CALIFICACION { get; set; }
    }
}
