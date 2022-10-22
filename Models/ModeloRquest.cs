using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SIGED_API.Models
{
    public class ModeloRquest
    {
        [Key]
        public int modelo_id { get; set; }

        public DateTime fecha_mod { get; set; }
        public int observador_id { get; set; }
        public int postulante_id { get; set; }
        public string Hora_inicial { get; set; }
        public string Hora_final { get; set; }

        public int area_id { get; set; }

        public string tema { get; set; }
        public string referencia { get; set; }
        public bool apreciacion { get; set; }

        [NotMapped]
        public IFormFile FrontImage { get; set; }
    }
}
