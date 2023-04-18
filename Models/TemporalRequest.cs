using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Models
{
    public class TemporalRequest
    {
        [NotMapped]
        public string? nombre { get; set; }
        public string? ape_paterno { get; set; }
        public string? ape_materno { get; set; }
        public string? tipo_id { get; set; }
        public string? numero { get; set; }
        public string? fec_nacimiento { get; set; }
        public string? celular { get; set; }
        public string? correo { get; set; }
        public string? contrasena { get; set; }
        public string? rep_contrasena { get; set; }
        public IFormFile? Imagen { get; set; }
        public IFormFile? Archivo { get; set; }
        public List<int>? listaEspecialidades { get; set; }
    }
  

}
