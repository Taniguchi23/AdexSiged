using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Rubrica_Detalle
    {
        [Key]
        public int Id { get; set; }
        public int Rubrica_id { get; set; }
        public string Descripcion { get; set; }
        public string Valoracion { get; set; }
        public decimal Puntaje { get; set; }
        public char Estado { get; set; }
    }
}
