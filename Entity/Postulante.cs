﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Postulante
    {
        [Key]
        public int  postulante_id { get; set; }
        public string ? nombre { get; set; }
        public string ? ape_paterno { get; set; }
        public string ? ape_materno { get; set; }
        public int  tipo_id { get; set; }
        public string ? numero { get; set; }
        public DateTime ? fec_nacimiento { get; set; }
        public string ? celular { get; set; }
        public string ? correo { get; set; }
        public string ? contrasena { get; set; }
        public string ? rep_contrasena { get; set; }
        public string ? imageurl { get; set; }

        public string ? archivocv { get; set; }

        public int rol_id { get; set; }

        public int seleccion_id { get; set; }

        public string ? estado { get; set; }

        public bool ? estado_contratado { get; set; }

        //[NotMapped]
        //public IFormFile FrontImage { get; set; }

        //[NotMapped]
        //public IFormFile FrontArchivo{ get; set; }


        //public List<Especialidad_postulante> Especialidades { get; set; }

        //public class Especialidad_postulante
        //{
        //    public int especialidad_id { get; set; }
        //    public int postulante_id { get; set; }
        //}
        //public string foto { get; set; }
        //public byte cv { get; set; }

        public string? genero { get; set; }

        public int? departamento_id { get; set; }

        public int? provincia_id { get; set; }

        public int? distrito_id { get; set; }

        public int? estado_id { get; set; }

        public string? correo_adex { get; set; }

        public string? telefono_fijo { get; set; }

        public string? telefono_emergencia { get; set; }

        public int? via_id { get; set; }

        public string? nombre_via { get; set; }

        public string? NroMzLote { get; set; }

        public string? interior { get; set; }

        public int? departamento_id_dir { get; set; }

        public int? provincia_id_dir { get; set; }

        public int? distrito_id_dir { get; set; }

        public bool? tiene_familiar { get; set; }

        public string? nombre_familiar { get; set; }

        public int? area_id { get; set; }

        public int? referido_linkedin { get; set; }
        public bool? referido_indeed { get; set; }
        public bool? referido { get; set; }

        public string? otros_medio { get; set; }

        public bool? persona_discapacidad { get; set; }

        public int? tipo_discapacidad_id { get; set; }

        public bool? certificado { get; set; }

        public string? num_certificado { get; set; }
    }
}
