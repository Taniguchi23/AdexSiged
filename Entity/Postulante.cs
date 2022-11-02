using Microsoft.AspNetCore.Http;
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
    }
}
