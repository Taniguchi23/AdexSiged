using System.ComponentModel.DataAnnotations;

namespace SIGED_API.Entity
{
    public class PAGO
    {
        [Key]
        public int PAGO_ID { get; set; }

        public int POSTULANTE_ID { get; set; }

        public string NRO_CUENTA { get; set; }

        public int BANCO_ID { get; set; }

        public string CCI { get; set; }

        public string SISTEMA_PEN { get; set; }

        public int AFP_ID { get; set; }

        public string OTROS_BANCOS { get; set; }
    }
}
