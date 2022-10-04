using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class PostulanteInformacion
    {
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
        public string area { get; set; }
        public int area_id { get; set; }
    }
}
