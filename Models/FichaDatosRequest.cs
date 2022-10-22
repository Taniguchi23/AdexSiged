using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Models
{
    public class FichaDatosRequest
    {
        [Key]

        public int estudio_id { get; set; }
        public int postulante_id { get; set; }

        public string otros { get; set; }

        public string otros_programas { get; set; }

        public string Pregrados { get; set; }
        public class Pregrado
        {
            public int pregrado_id { get; set; }

            public int estudio_id { get; set; }
            public string centro_estudio { get; set; }
            public string carrera { get; set; }
            public int grado_acad { get; set; }
            public DateTime fecha_ingreso { get; set; }

            public DateTime fecha_salida { get; set; }
        }


        public string Postgrados { get; set; }
        public class Postgrado
        {
            public int postgrado_id { get; set; }

            public int estudio_id { get; set; }
            public string centro_estudio { get; set; }
            public string especializacion { get; set; }
            public int nivel { get; set; }
            public DateTime fecha_ingreso { get; set; }

            public DateTime fecha_salida { get; set; }
        }

        public string Idioma_Ingles { get; set; }
        public class NIVEL_INGLES
        {
            public int NIVEL_INGLES_ID { get; set; }

            public int ESTUDIO_ID { get; set; }

            public int IDIOMA_ID { get; set; }

            public int NIVELESTUDIO_ID { get; set; }
        }

        public string Ofimatica { get; set; }
        public class NIVEL_OFIMATICA
        {
            public int NIVEL_OFIMATICA_ID { get; set; }

            public int ESTUDIO_ID { get; set; }

            public int OFIMATICA_ID { get; set; }

            public int NIVELESTUDIO_ID { get; set; }
        }


        public string Experiencia { get; set; }
        public class EXPERIENCIA_LABORAL
        {
            public int EXPERIENCIA_LABORAL_ID { get; set; }

            public int EXPERIENCIA_ID { get; set; }

            public string EMPRESA { get; set; }

            public string CARGO { get; set; }

            public string JEFE_INMEDIATO { get; set; }

            public string TELEFONO { get; set; }

            public DateTime FECHA_INGRESO { get; set; }

            public DateTime FECHA_CESE { get; set; }

            public string MOTIVO_CESE { get; set; }
        }


        public int COMPOSICION_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public string NOMBRE { get; set; }

        public string APELLIDO_PATERNO { get; set; }

        public string APELLIDO_MATERNO { get; set; }

        public string DNI { get; set; }

        public DateTime FECHA { get; set; }

        public int EDAD { get; set; }


        public string Hijos { get; set; }
        public class COMPOSICION_HIJO
        {
            [Key]
            public int COMPOSICION_HIJO_ID { get; set; }

            public int COMPOSICION_ID { get; set; }

            public string NOMBRE { get; set; }

            public string APELLIDO_PATERNO { get; set; }

            public string APELLIDO_MATERNO { get; set; }

            public string DNI { get; set; }

            public DateTime FECHA { get; set; }

            public int EDAD { get; set; }
        }


        [NotMapped]
        public IFormFile FrontImage { get; set; }
    }
}
