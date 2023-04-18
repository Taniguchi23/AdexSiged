using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Entity
{
    public class TEMPORAL_IMAGEN
    {
        [Key]
        public int TEMP_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public string ARCHIVO { get; set; }

        public int MODULO { get; set; }


        public int TIPOARCHIVO { get; set; }

      /*  [NotMapped]
        public IFormFile? FrontArchivo { get; set; }*/
    }
}
