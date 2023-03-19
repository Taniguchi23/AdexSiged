using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class Especialidad_cursos
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Ciclo { get; set; }
        public int Especialidad_id { get; set; }
    }
}
