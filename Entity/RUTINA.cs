using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class RUTINA
    {

        [Key]
        public int RUTINA_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public int GRUPO { get; set; }

        public bool ESTADO { get; set; }
    }
}
