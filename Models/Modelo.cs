using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Models
{
    public class Modelo
    {
        [Key]
        public int modelo_id { get; set; }

        public DateTime fecha_mod { get; set; }
        public int? observador_id { get; set; }
        public string? nombre_observador { get; set; }
        public int postulante_id { get; set; }
        public string Hora_inicial { get; set; }
        public string Hora_final { get; set; }

        public int area_id { get; set; }

        public string tema { get; set; }
        public string referencia { get; set; }
        public bool apreciacion { get; set; }

        public int max_puntaje { get; set; }

        [NotMapped]
        public IFormFile FrontImage { get; set; }
    }
}
