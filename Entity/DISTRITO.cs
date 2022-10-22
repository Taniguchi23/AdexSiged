using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class DISTRITO
    {
        [Key]
        public int DISTRITO_ID { get; set; }

        public string DISTRITO_NOM { get; set; }

        public int PROVINCIA_ID { get; set; }
    }
}
