namespace SIGED_API.Models.Response
{
    public class Roles
    {

        public bool status { get; set; }
        public string message { get; set; }
        public string fullname { get; set; }
        public int rol { get; set; }
        public string descripcion { get; set; }
        public string token { get; set; }
        public string? correo { get; set; }
        public string? password { get; set; }
        public string? user { get; set; }
        public int? id { get; set; }
        public int? subRol { get; set; }
        public string? descripcionSubRol { get; set; }


    }
}
