using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit.Net.Smtp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/notificacion")]
    [ApiController]
    [Authorize]
    public class NotificacionController : ControllerBase
    {

        private readonly AppDbContext2 context;

        public NotificacionController(AppDbContext2 context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<NotificacionController>
        [HttpGet]
        public IEnumerable<NOTIFICACION> Get()
        {

            return context.NOTIFICACION.ToList();

        }


        // GET api/<NotificacionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<NotificacionController>
        //[HttpPost]
        //public int GrabarReporte([FromBody] NOTIFICACION notificacion)
        //{

        //    var vnotificacion = context.NOTIFICACION.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);

        //    if (vnotificacion != null)
        //    {
        //        return vnotificacion.notificacion_id;
        //    }
        //    else
        //    {
        //        NOTIFICACION oreporte = new NOTIFICACION();
        //        oreporte.fecha = notificacion.fecha;
        //        oreporte.postulante_id = notificacion.postulante_id;
        //        oreporte.fecha = notificacion.fecha;
        //        oreporte.estado = notificacion.estado;
        //        context.NOTIFICACION.Add(oreporte);
        //        context.SaveChanges();

        //        return oreporte.notificacion_id;
        //    }


        //}


        //[HttpPost("EnviarNotificacion/{id}")]
        //public string Notificar([FromBody] NOTIFICACION notificacion)
        //{

        //    return "value";
        //}

        [HttpPost("NotificacionInducciones")]
        public string Notificar([FromBody] NOTIFICACION notificacion)
        {
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 1);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
            email.To.Add(MailboxAddress.Parse(vpostulante.correo));
            email.Subject = vnotificacion.asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = "<p>!Queremos darte la bienvenida al Staff de docentes del Instituto ADEX y decirte que, estamos seguros que ti profesionalismo aportara a nuestra instituicon en favor de la educacion y el aprendizaje!</p><p>  A fin de iniciar actividades de capacion e induccion docente, se le informa lo siguiente:</p> <p>  1. Pongo en copia al area de Soporte TI y Programacion Academica a fin de activar sus acccesos a las plataformas: Licencia Zoom, Aula Virtual e Intranet(Power Campus). Adjuntamos el formato de disponibilidad con sus datos iniciales.</p>  <p>   2. Usted iniciara actividades academicas a partir del dia "+ notificacion.fecha_actividad.ToString("dddd") + " " + notificacion.fecha_actividad.ToString("dd") + " de " + notificacion.fecha_actividad.ToString("MMMM") + ". En los proximos dias se le informara sobre su programacion horario, la misma que podra visualizaria en el sistema Power Campus.</p><p>  3. Se lea agregara al grupo de whasatapp \"Super Docente ADEX\" que tienes como fin compartir informacion sobres buenas practicas docentes</p><p> 4. Se le inscribira al curso Gimnsaio Virtual Docente en el Aula Virtual(habilitado del " + notificacion.fecha_desde.ToString("dd") + " " + notificacion.fecha_desde.ToString("MMMM") +  " al  "+ notificacion.fecha_hasta.ToString("dd")  + " " + notificacion.fecha_hasta.ToString("MMMM") + ") y a un curso de prueba donde podra poner en practica lo aprendido a fin de que pueda dominar las plataformas de ADEX.</p><p> Saludos cordiales</p><p><b> Gestion Docente</b></p>" };
        
            using  var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("ronald.livia@outlook.com", "Yama314162$");
            smtp.Send(email);
            smtp.Disconnect(true);
            return  "OK";
        }


        [HttpPost("NotificacionDesaprobado")]
        public string NotificarDesaprobado([FromBody] NOTIFICACION notificacion)
        {
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 3);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
            email.To.Add(MailboxAddress.Parse(vpostulante.correo));
            email.Subject = vnotificacion.asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = vnotificacion.mensaje };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("ronald.livia@outlook.com", "Yama314162$");
            smtp.Send(email);
            smtp.Disconnect(true);
            return "OK";
        }

        [HttpPost("NotificacionAprobado")]
        public string NotificarAprobado([FromBody] NOTIFICACION notificacion)
        {
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 5);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
            email.To.Add(MailboxAddress.Parse(vpostulante.correo));
            email.Subject = vnotificacion.asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = vnotificacion.mensaje };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("ronald.livia@outlook.com", "Yama314162$");
            smtp.Send(email);
            smtp.Disconnect(true);
            return "OK";
        }
    }
}
