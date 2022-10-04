using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Entity
{
    public class Login
    {
        [Key]
        public string correo   { get; set; }
        public string contrasena { get; set; }
    }
}
