using SIGED_API.Entity;
using System.Collections.Generic;

namespace SIGED_API.Models.Dao
{
    public class RubricaDao
    {
        public Rubrica Rubrica { get; set; }
        public List<Rubrica_Detalle>? ListaRubrica { get; set; }
        public List<decimal>? ListaPuntajes { get; set; }
    }
}
