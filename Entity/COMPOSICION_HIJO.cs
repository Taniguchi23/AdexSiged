﻿using System.ComponentModel.DataAnnotations;
using System;

namespace SIGED_API.Entity
{
    public class COMPOSICION_HIJO
    {
        [Key]
        public int COMPOSICION_HIJO_ID { get; set; }

        public int COMPOSICION_ID { get; set; }

        public string NOMBRE { get; set; }

        public string APELLIDO_PATERNO { get; set; }

        public string APELLIDO_MATERNO { get; set; }

        public string DNI { get; set; }

        public DateTime FECHA { get; set; }

        public int EDAD { get; set; }
    }
}
