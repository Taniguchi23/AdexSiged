using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class ENVIAR_CORREO
    {
        [Key]
        public int envio_id { get; set; }
        public string evento { get; set; }
        public string destinatario { get; set; }
        public string destinatariocc { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
    }
}
