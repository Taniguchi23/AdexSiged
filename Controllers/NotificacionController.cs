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

        [HttpPost("EnviarNotificacion")]
        public string Notificar([FromBody] NOTIFICACION notificacion)
        {
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 1);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
            email.To.Add(MailboxAddress.Parse(vpostulante.correo));
            email.Subject = vnotificacion.asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = vnotificacion.mensaje };
        
            using  var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("ronald.livia@outlook.com", "Yama314162$");
            smtp.Send(email);
            smtp.Disconnect(true);
            return  "OK";
        }


        // PUT api/<NotificacionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<NotificacionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
