using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGED_API.Models
{
    public class TemporalRequest
    {
        [NotMapped]
        public IFormFile FrontArchivo { get; set; }
    }
}
