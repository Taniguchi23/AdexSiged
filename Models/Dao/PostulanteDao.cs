using System;

namespace SIGED_API.Models.Dao
{
    public class PostulanteDao
    {
        public int postulante_id { get; set; }
        public string? nombre { get; set; }
        public string? aoe_paterno { get; set; }
        public string? ape_materno { get; set; }
        public string? correo { get; set; }
        public string? imagerul { get; set; }
        public int? seleccion_id { get; set; }
        public int? rol_id { get; set; }
        public Boolean? estado_contratado { get; set; }

    }
}
