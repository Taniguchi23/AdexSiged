using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Rubrica_Modelo
    {
        [Key]
        public int rubrica_mod_id { get; set; }

        public int rubrica_id { get; set; }
        public int modelo_id { get; set; }
        public int puntaje { get; set; }
    }
}
