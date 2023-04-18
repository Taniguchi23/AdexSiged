using Microsoft.AspNetCore.Http;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Models
{
    public class FichaDatosRequest
    {
        [Key]

        public int postulante_id { get; set; }
        public string nombre { get; set; }
        public string ape_paterno { get; set; }
        public string ape_materno { get; set; }
        public string dni { get; set; }
        public DateTime fec_nacimiento { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rep_contrasena { get; set; }

        public string imageurl { get; set; }

        public string archivocv { get; set; }

        public string genero { get; set; }

        public int departamento_id { get; set; }

        public int provincia_id { get; set; }

        public int distrito_id { get; set; }

        public int estado_id { get; set; }

        public string correo_adex { get; set; }

        public string telefono_fijo { get; set; }

        public string telefono_emergencia { get; set; }

        public int via_id { get; set; }

        public string nombre_via { get; set; }

        public string NroMzLote { get; set; }

        public string interior { get; set; }

        public int departamento_id_dir { get; set; }

        public int provincia_id_dir { get; set; }

        public int distrito_id_dir { get; set; }

        public bool tiene_familiar { get; set; }

        public string nombre_familiar { get; set; }

        public int area_id { get; set; }

        public bool referido_linkedin { get; set; }

        public bool referido_indeed { get; set; }

        public bool referido { get; set; }


        public string otros_medio { get; set; }

        public bool persona_discapacidad { get; set; }

        public int tipo_discapacidad_id { get; set; }

        public bool certificado { get; set; }

        public string num_certificado { get; set; }


        public int estudio_id { get; set; }

        public string otros { get; set; }

        public string otros_programas { get; set; }

        public List<Pregrado> Pregrados { get; set; }
     /*   public class Pregrado
        {
            public int pregrado_id { get; set; }

            public int estudio_id { get; set; }
            public string centro_estudio { get; set; }
            public string carrera { get; set; }
            public int grado_acad { get; set; }
            public DateTime fecha_ingreso { get; set; }

            public DateTime fecha_salida { get; set; }
        */


        public List<Postgrado> Postgrados { get; set; }
      /*  public class Postgrado
        {
            public int postgrado_id { get; set; }

            public int estudio_id { get; set; }
            public string centro_estudio { get; set; }
            public string especializacion { get; set; }
            public int nivel { get; set; }
            public DateTime fecha_ingreso { get; set; }

            public DateTime fecha_salida { get; set; }
        }*/

        public List<NIVEL_INGLES> Idioma_Ingles { get; set; }
     /*   public class NIVEL_INGLES
        {
            public int nivel_ingles_id { get; set; }

            public int estudio_id { get; set; }

            public int idioma_id { get; set; }

            public int nivelestudio_id { get; set; }
        }*/

        public List<NIVEL_OFIMATICA> Ofimatica { get; set; }

     /*   public class NIVEL_OFIMATICA
        {
            public int nivel_ofimatica_id { get; set; }

            public int estudio_id { get; set; }

            public int ofimatica_id { get; set; }

            public int nivelestudio_id { get; set; }
        }*/

        public List<EXPERIENCIA_LABORAL> Experiencia { get; set; }
      /*  public class EXPERIENCIA_LABORAL
        {
            public int experiencia_laboral_id { get; set; }

            public int experiencia_id { get; set; }

            public string empresa { get; set; }

            public string cargo { get; set; }

            public string jefe_inmediato { get; set; }

            public string telefono { get; set; }

            public DateTime fecha_ingreso { get; set; }

            public DateTime fecha_cese { get; set; }

            public string motivo_cese { get; set; }

            public bool autorizo_contactar { get; set; }
        }*/


        public int composicion_id { get; set; }

        public string nombre_padre { get; set; }

        public string apellido_paterno { get; set; }

        public string apellido_materno { get; set; }

        public string dni_padre { get; set; }

        public DateTime fecha { get; set; }

        public int edad { get; set; }


        public List<COMPOSICION_HIJO> Hijos { get; set; }
      /*  public class COMPOSICION_HIJO
        {
            public int composicion_hijo_id { get; set; }

            public int composicion_id { get; set; }

            public string nombre { get; set; }

            public string apellido_paterno { get; set; }

            public string apellido_materno { get; set; }

            public string dni { get; set; }

            public DateTime fecha { get; set; }

            public int edad { get; set; }
        }*/

        public int pago_id { get; set; }

        public string nro_cuenta { get; set; }

        public int banco_id { get; set; }

        public string cci { get; set; }

        public string sistema_pen { get; set; }

        public string otrosbancos { get; set; }

        public int afp_id { get; set; }

        

      
    }
}
