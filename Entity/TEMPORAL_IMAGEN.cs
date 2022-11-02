using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Entity
{
    public class TEMPORAL_IMAGEN
    {
        [Key]
        public int temp_id { get; set; }

        public string descripcion { get; set; }

        public string archivo { get; set; }

        public int modulo { get; set; }


        public int tipoarchivo { get; set; }

        [NotMapped]
        public IFormFile FrontArchivo { get; set; }
    }
}
