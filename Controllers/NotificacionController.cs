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
using System;
using SIGED_API.Models;
using SIGED_API.Ficha;

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
        public ActionResult Notificar([FromBody] NOTIFICACION notificacion)
        {
            var result = new OkObjectResult(0);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);
            try
            {
                var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 1);

             

                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
                email.To.Add(MailboxAddress.Parse(vpostulante.correo));
                email.Subject = vnotificacion.asunto;
                email.Body = new TextPart(TextFormat.Html) { Text = "<p>¡Queremos darte la bienvenida a la Plana docente del Instituto ADEX y decirte que, estamos seguros que tu profesionalismo aportará a nuestra institución en favor de la educación y el aprendizaje!</p><p> A fin de iniciar actividades de capacitación e inducción docente, se le informa lo siguiente:</p> <p>  1. Pongo en copia al área de Soporte TI y Programación Académica a fin de activar sus accesos a las plataformas: Licencia Zoom, Aula Virtual e Intranet (Power Campus). Adjuntamos el formato de disponibilidad con sus datos iniciales.</p>  <p>   2. Usted iniciará actividades académicas a partir del día " + notificacion.fecha_actividad.ToString("dddd") + " " + notificacion.fecha_actividad.ToString("dd") + " de " + notificacion.fecha_actividad.ToString("MMMM") + ". En los próximos días se le informará sobre su programación horaria, la misma que podrá visualizar en el sistema Power Campus.</p><p>  3. Se le agregará al grupo de WhatsApp \"Súper Docente ADEX\" que tiene como fin compartir información sobre buenas prácticas docentes.</p><p> 4. Se le inscribirá al curso Gimnasio Virtual Docente en el Aula Virtual (habilitado del " + notificacion.fecha_desde.ToString("dd") + " " + notificacion.fecha_desde.ToString("MMMM") + " al  " + notificacion.fecha_hasta.ToString("dd") + " " + notificacion.fecha_hasta.ToString("MMMM") + ") y a un curso de prueba donde podrá poner en práctica lo aprendido a fin de que pueda dominar las plataformas de ADEX.</p><p> Saludos cordiales </p><p><b> Gestión Docente</b></p>" };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("ronald.livia@outlook.com", "Yama314162$");
                smtp.Send(email);
                smtp.Disconnect(true);

                result = new OkObjectResult(new { message = "OK", status = true, postulante_id = notificacion.postulante_id });
                return result;
            }
            catch (Exception ex)
            {

                 result = new OkObjectResult(new { essage = "NOOK", status = false, postulante_id = notificacion.postulante_id , full_name = vpostulante.nombre + ' ' + vpostulante.ape_paterno });
                return result;
            } 

            
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
