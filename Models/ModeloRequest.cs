using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Models
{
    public class ModeloRequest
    {
        [Key]
        public int modelo_id { get; set; }



        public string Rubricas_Modelo { get; set; }
        public class Rubrica_Modelo
        {
            public int rubrica_mod_id { get; set; }

            public int id_rubrica { get; set; }
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

       

       
    }
}
