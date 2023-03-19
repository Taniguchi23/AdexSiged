using SIGED_API.Models.Dao;
using System;

namespace SIGED_API.Models.Response
{
    public class PostulanteEvaluacion
    {
        public int postulante_id { get; set; }
        public string? flagTipo { get; set; }
        public string? nombre { get; set; }
        public string? ape_paterno { get; set; }
        public string? ape_materno { get; set; }
        public string? correo { get; set; }
        public string? imagerul { get; set; }
        public int? seleccion_id { get; set; }
        public int? rol_id { get; set; }
        public Boolean? estado_contratado { get; set; }
        public int? detalle_evaluacion_id { get; set; }
        
        public int? enc_estu { get; set; }
        public int? cum_adm { get; set; }
        public int? cap_doc { get; set; }
        public int? acom_doc { get; set; }
        public int? cum_vir { get; set; }
        public decimal? nota_final { get; set; }
    }

    

    
}
