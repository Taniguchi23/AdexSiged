using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Rubrica
    {
        [Key]
        public int rubrica_id { get; set; }
        public string  competencia { get; set; }
        public string criterio { get; set; }
    }
}
