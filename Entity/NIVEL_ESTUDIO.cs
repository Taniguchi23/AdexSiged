using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class NIVEL_ESTUDIO
    {
        [Key]
        public int NIVELESTUDIO_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public bool ESTADO { get; set; }


    }
}
