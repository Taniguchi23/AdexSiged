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
using SIGED_API.Models.Dao;
using SIGED_API.Models.Response;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using SIGED_API.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/programacion")]
    [ApiController]
   // [Authorize]
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
        public Respuesta Get()
        {
            var respuesta = new Respuesta();
            respuesta.status = true;

            var listaProgramacion = from p in context.PROGRAMACION
                                    join po in context.Postulante on p.POSTULANTE_ID equals po.postulante_id
                                    where p.ESTADO != "R"
                                    select new DetalleProgramacion()
                                    {
                                        nombre_completo = po.nombre + " " + po.ape_paterno + " " + po.ape_materno,
                                        postulante_id = po.postulante_id,
                                        fecha = p.FECHA,
                                        created_at = p.CREATED_AT,
                                        estado = p.ESTADO
                                    };
            foreach (DetalleProgramacion detalleProgramacion in listaProgramacion)
            {

                if (detalleProgramacion.estado == "EV") {
                    detalleProgramacion.estado = "EV";
                }
                else if(detalleProgramacion.estado == "PR")
                {
                    DateTime fechaHoraUtc = DateTime.UtcNow;
                    TimeZoneInfo zonaHorariaUtc5 = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                    DateTime fechaHoraUtc5 = TimeZoneInfo.ConvertTime(fechaHoraUtc, zonaHorariaUtc5);
                    DateTime fechaHoraUtc5ConMilisegundos = new DateTime(fechaHoraUtc5.Ticks - (fechaHoraUtc5.Ticks % TimeSpan.TicksPerMillisecond), fechaHoraUtc5.Kind);

                    if (detalleProgramacion.fecha < fechaHoraUtc5ConMilisegundos)
                    {
                        detalleProgramacion.estado = "PE";
                    }
                }
                else
                {
                    detalleProgramacion.estado = "RE";
                }

             
            }

            respuesta.Data = listaProgramacion;
            return respuesta;

          
            
        }

        // GET api/<ProgramacionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProgramacionController>
        [HttpPost("ProgramacionEvaluaciones")]
        public Respuesta Programacion([FromBody] ProgramacionDao programacionDao)
        {
            var respuesta = new Respuesta();
            respuesta.status = false;
            DateTime fechaHoraUtc = DateTime.UtcNow;
            TimeZoneInfo zonaHorariaUtc5 = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime fechaHoraUtc5 = TimeZoneInfo.ConvertTime(fechaHoraUtc, zonaHorariaUtc5);
            DateTime fechaHoraUtc5ConMilisegundos = new DateTime(fechaHoraUtc5.Ticks - (fechaHoraUtc5.Ticks % TimeSpan.TicksPerMillisecond), fechaHoraUtc5.Kind);


            if (fechaHoraUtc5ConMilisegundos > programacionDao.fecha)
            {
                respuesta.Data = "La fecha es menor a la hora actual.";
                return respuesta;
            }

            var programacion = context.PROGRAMACION.FirstOrDefault(p => p.POSTULANTE_ID == programacionDao.postulante_id && p.ESTADO != "Ev");
            if(programacion != null)
            {
                programacion.FECHA = programacionDao.fecha;
                programacion.ESTADO = "PR";
                context.Entry(programacion).State = EntityState.Modified;
                context.SaveChanges();
                respuesta.Data = "Se ha actualizado la fecha de programación.";
                respuesta.status = true;
                return respuesta;
            }

            PROGRAMACION oreporte = new PROGRAMACION();
            oreporte.FECHA = programacionDao.fecha;
            oreporte.CREATED_AT = programacionDao.created_at;
            oreporte.POSTULANTE_ID = programacionDao.postulante_id;
            oreporte.ESTADO = "PR";
            context.PROGRAMACION.Add(oreporte);
            context.SaveChanges();

            NotificarCorreo(programacionDao.postulante_id, programacionDao.fecha);
            respuesta.status = true;
            respuesta.Data = "Programación creada con éxito";
            return respuesta;
        }


        [HttpGet("ConsultarDocente/{docente}")]
        public IEnumerable<DetalleProgramacion> ConsultarDocente(string docente)
        {
            List<DetalleProgramacion> evaluacion = new List<DetalleProgramacion>();



            evaluacion = context.PROGRAMACION.Join(context.Postulante,
               sd => sd.POSTULANTE_ID,
               r => r.postulante_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.r.nombre.Contains(docente))
               .Select(res => new DetalleProgramacion()
               {
                   postulante_id = res.r.postulante_id,
                   nombre_completo = res.r.ape_paterno + " " + res.r.ape_materno + " " + res.r.nombre,
                   estado = res.r.estado

               }).ToList();

        

            return evaluacion;


        }

        [HttpGet("ConsultarDocente/{fechadesde}/{fechahasta}")]
        public IEnumerable<DetalleProgramacion> ConsultarFechas(string fechadesde, string fechahasta)
        {
            
            DateTime fechades = Convert.ToDateTime(fechadesde);
            DateTime fechahas = Convert.ToDateTime(fechahasta);

            List<DetalleProgramacion> evaluacion = new List<DetalleProgramacion>();


           evaluacion = context.PROGRAMACION.Join(context.Postulante,
               sd => sd.POSTULANTE_ID,
               r => r.postulante_id,
               (sd, r) => new { sd, r }
           ).Where(c => c.sd.FECHA >= fechades && c.sd.FECHA <= fechahas)
               .Select(res => new DetalleProgramacion()
               {
                   postulante_id = res.r.postulante_id,
                   nombre_completo = res.r.ape_paterno + " " + res.r.ape_materno + " " + res.r.nombre,
                   estado = res.r.estado

               }).ToList();

            return evaluacion;

        }

        private void NotificarCorreo(int postulante_id, DateTime fecha)
        {
            int numero = 0;
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 4);

            var vpostulante = context.Postulante.FirstOrDefault(p => p.postulante_id == postulante_id);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(vnotificacion.destinatario));
            email.To.Add(MailboxAddress.Parse(vpostulante.correo));
            email.Subject = vnotificacion.asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = " <p>  Estimado docente, le informamos que la programación de su evaluación del Gimnasio Virtual será el día " + fecha.ToString("dd") + " de " + fecha.ToString("MMMM") + " a las "+ fecha.ToString("hh:mm") + " horas.  </p> <p>Favor de confirmar la recepción del presente.   </p> <p>Gracias por su amable atención.  </p> <p>Saludos cordiales    </p>   <p><b>Gestión Docente   </b>  </p>" };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(Settings.smtp, Settings.puerto, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(Settings.email, Settings.password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

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
