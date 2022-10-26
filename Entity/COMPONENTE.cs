using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class COMPONENTE
    {
        [Key]
        public int COMPONENTE_ID { get; set; }

        public string DESCRIPCION { get; set; }

        public int PORCENTAJE { get; set; }

        public bool ESTADO { get; set; }


    }
}
