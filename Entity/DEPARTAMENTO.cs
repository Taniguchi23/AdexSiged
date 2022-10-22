using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class DEPARTAMENTO
    {
        [Key]
        public int DEPARTAMENTO_ID { get; set; }

        public string DEPARTAMENTO_NOM { get; set; }

        public int PAIS_ID { get; set; }
    }
}
