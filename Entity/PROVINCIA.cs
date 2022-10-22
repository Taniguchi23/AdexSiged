using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class PROVINCIA
    {
        [Key]
        public int PROVINCIA_ID { get; set; }

        public string PROVINCIA_NOM { get; set; }

        public int DEPARTAMENTO_ID { get; set; }
    }
}
