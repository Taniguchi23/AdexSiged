using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Seleccion_detalle
    {
        [Key]
        public int sel_detalle_id { get; set; }
        public int seleccion_id { get; set; }
        public int revision_id { get; set; }
        public int e_competencia_id { get; set; }
        public int e_tecnica_id { get; set; }
        public int entrevistaja_id { get; set; }

    }
}
