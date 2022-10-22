using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SIGED_API.Models
{
    public class ModeloDetalleRequest
    {
        [Key]
        public int modelo_id { get; set; }
        

        public string Rubricas_Modelo { get; set; }
        public class Rubrica_Modelo
        {
            public int rubrica_mod_id { get; set; }
            public int rubrica_id { get; set; }
            public int modelo_id { get; set; }
            public int puntaje { get; set; }
        }
        public string Fortalezas { get; set; }

        public class Fortaleza
        {
            public int fortaleza_id { get; set; }

            public int modelo_id { get; set; }

            public string descripcion { get; set; }
        }

        public string Oportunidades { get; set; }

        public class Oportunidad
        {
            public int oportunidad_id { get; set; }

            public int modelo_id { get; set; }

            public string descripcion { get; set; }
        }


        public DateTime fecha_mod { get; set; }
        public int observador_id { get; set; }
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
