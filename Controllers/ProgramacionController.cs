using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using SIGED_API.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Postulante = SIGED_API.Entity.Postulante;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/programacion")]
    [ApiController]
    [Authorize]
    public class ProgramacionController : ControllerBase
    {

        private readonly AppDbContext2 context;

        public ProgramacionController(AppDbContext2 context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ProgramacionController>
        [HttpGet]
        public IEnumerable<PROGRAMACION> Get()
        {

            return context.PROGRAMACION.ToList();

        }

        // GET api/<ProgramacionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProgramacionController>
        [HttpPost("ProgramacionEvaluaciones")]
        public ActionResult Programacion([FromBody] PROGRAMACION programacion)
        {
            var result = new OkObjectResult(0);

            try
            {

                PROGRAMACION oreporte = new PROGRAMACION();
                oreporte.fecha = programacion.fecha;
                oreporte.postulante_id = programacion.postulante_id;
                context.PROGRAMACION.Add(oreporte);
                context.SaveChanges();

                NotificarCorreo(programacion.postulante_id, programacion.fecha);
                result = new OkObjectResult(new { message = "OK", status = true, programacion_id = oreporte.programacion_id });

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
         

        }


        [HttpGet("ConsultarDocente/{docente}")]
        public Postulante ConsultarDocente(string docente)
        {


            Postulante postulante = new Postulante();

            postulante = context.PROGRAMACION.Join(context.Postulante,
               sd => sd.postulante_id,
               r => r.postulante_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.r.nombre.Contains(docente))
               .Select(res => new Postulante()
               {
                   postulante_id = res.r.postulante_id,
                   nombre = res.r.nombre,
                   ape_paterno = res.r.ape_paterno,
                   ape_materno = res.r.ape_materno,
                   tipo_id = res.r.tipo_id,
                   numero = res.r.numero,
                   fec_nacimiento = res.r.fec_nacimiento,
                   celular = res.r.celular,
                   contrasena = Encrypt.GetSHA256(res.r.contrasena),
                   rep_contrasena = Encrypt.GetSHA256(res.r.rep_contrasena),
                   imageurl = res.r.imageurl,
                   archivocv = res.r.archivocv,
                   rol_id = res.r.rol_id,           
                   correo = res.r.correo,
                   estado = res.r.estado

               }).FirstOrDefault();

        

            return postulante;


        }

        [HttpGet("ConsultarDocente/{fechadesde}/{fechahasta}")]
        public Postulante ConsultarFechas(string fechadesde, string fechahasta)
        {
            Postulante postulante = new Postulante();
            DateTime fechades = Convert.ToDateTime(fechadesde);
            DateTime fechahas = Convert.ToDateTime(fechadesde);
            postulante = context.PROGRAMACION.Join(context.Postulante,
               sd => sd.postulante_id,
               r => r.postulante_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.sd.fecha >= fechades && c.sd.fecha <= fechahas)
               .Select(res => new Postulante()
               {
                   postulante_id = res.r.postulante_id,
                   nombre = res.r.nombre,
                   ape_paterno = res.r.ape_paterno,
                   ape_materno = res.r.ape_materno,
                   tipo_id = res.r.tipo_id,
                   numero = res.r.numero,
                   fec_nacimiento = res.r.fec_nacimiento,
                   celular = res.r.celular,
                   contrasena = Encrypt.GetSHA256(res.r.contrasena),
                   rep_contrasena = Encrypt.GetSHA256(res.r.rep_contrasena),
                   imageurl = res.r.imageurl,
                   archivocv = res.r.archivocv,
                   rol_id = res.r.rol_id,
                   correo = res.r.correo,
                   estado = res.r.estado

               }).FirstOrDefault();



            return postulante;


        }

        private void NotificarCorreo(int postulante_id, DateTime fecha)
        {
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 4);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == postulante_id);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
            email.To.Add(MailboxAddress.Parse(vpostulante.correo));
            email.Subject = vnotificacion.asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = " <p>  Estimado docente, le informamos que la programación de su evaluación del Gimnasio Virtual será el día " + fecha.ToString("dd") + " de " + fecha.ToString("MMMM") + " a las 18:00 horas.  </p> <p>Favor de confirmar la recepción del presente.   </p> <p>Gracias por su amable atención.  </p> <p>Saludos cordiales    </p>   <p><b>Gestión Docente   </b>  </p>" };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("ronald.livia@outlook.com", "Yama314162$");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        // PUT api/<ProgramacionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProgramacionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
