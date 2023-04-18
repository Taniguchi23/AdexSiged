namespace SIGED_API.Models.Request
{
    public class VerificarRecuperarClaveRequest
    {
        public int IdPostulante { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
    }
}
