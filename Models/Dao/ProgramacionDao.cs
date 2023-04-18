using System;

namespace SIGED_API.Models.Dao
{
    public class ProgramacionDao
    {
        public int postulante_id { get; set; }
        public DateTime fecha { get; set; }
        public DateTime created_at { get; set; }
        public string? estado { get; set;}
    }
}
