using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;


using System.Threading.Tasks;
using SIGED_API.Models;
using Newtonsoft.Json;
using Postulante = SIGED_API.Entity.Postulante;
using Postulantes = SIGED_API.Models.Postulante;
using Microsoft.AspNetCore.Authorization;
using SIGED_API.Tools;
using MimeKit.Text;
using MimeKit;
using SIGED_API.Ficha;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using SIGED_API.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/postulante")]
    [ApiController]


    public class PostulanteController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly AppDbContext2 context2;
        private readonly AppDbContext4 context4;
        private readonly ILogger<PostulanteController> logger;
        private readonly IWebHostEnvironment webHostEnviroment;
        public PostulanteController(AppDbContext context, AppDbContext2 context2, AppDbContext4 context4, ILogger<PostulanteController> logger, IWebHostEnvironment webHost)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
            this.context2 = context2;
            this.context4 = context4;
            webHostEnviroment = webHost;
        }

        [HttpPost]
        public async Task<Respuesta> Post([FromForm] TemporalRequest temporalRequest)
        {
            var respuesta = new Respuesta();
            var listaErrores = new List<Error>();
            respuesta.status = false;
            var postulanteTemp = context.Postulante.FirstOrDefault(p => p.correo == temporalRequest.correo || p.numero == temporalRequest.numero);
            if (postulanteTemp != null)
            {
                if (postulanteTemp.numero.Trim() == temporalRequest.numero) listaErrores.Add(new Error() { Campo = "numero", Detalles = "El numero de documento ya existe" });
                if (postulanteTemp.correo.Trim() == temporalRequest.correo) listaErrores.Add(new Error() { Campo = "correo", Detalles = "El correo ya existe" });
                respuesta.Data = listaErrores;
                return respuesta;
            }

            if (temporalRequest.Archivo == null || temporalRequest.Archivo.Length == 0)
                listaErrores.Add(new Error() { Campo = "archivo", Detalles = "No se ha enviado el campo archivo" });

            if (temporalRequest.Imagen == null || temporalRequest.Imagen.Length == 0)
                listaErrores.Add(new Error() { Campo = "imagen", Detalles = "No se ha enviado el campo imagen" });

            if (temporalRequest.nombre == null)
                listaErrores.Add(new Error() { Campo = "nombre", Detalles = "No se ha enviado el campo nombre" });

            if (temporalRequest.ape_paterno == null)
                listaErrores.Add(new Error() { Campo = "ape_paterno", Detalles = "No se ha enviado el campo ape_paterno" });

            if (temporalRequest.ape_materno == null)
                listaErrores.Add(new Error() { Campo = "ape_materno", Detalles = "No se ha enviado el campo ape_materno" });

            if (temporalRequest.tipo_id == null)
                listaErrores.Add(new Error() { Campo = "tipo_id", Detalles = "No se ha enviado el campo tipo_id" });

            if (temporalRequest.numero == null)
                listaErrores.Add(new Error() { Campo = "numero", Detalles = "No se ha enviado el campo numero" });

            if (temporalRequest.fec_nacimiento == null)
                listaErrores.Add(new Error() { Campo = "fec_nacimiento", Detalles = "No se ha enviado el campo fec_nacimiento" });

            if (temporalRequest.celular == null)
                listaErrores.Add(new Error() { Campo = "celular", Detalles = "No se ha enviado el campo celular" });

            if (temporalRequest.correo == null)
                listaErrores.Add(new Error() { Campo = "correo", Detalles = "No se ha enviado el campo correo" });

            if (temporalRequest.contrasena == null)
                listaErrores.Add(new Error() { Campo = "contrasena", Detalles = "No se ha enviado el campo contrasena" });

            DateTime fecha;
            if (temporalRequest.fec_nacimiento != null && !DateTime.TryParse(temporalRequest.fec_nacimiento, out fecha))
                listaErrores.Add(new Error() { Campo = "fecha", Detalles = "el campo fecha no contiene un formato fecha" });

            if (listaErrores.Count() > 0)
            {
                respuesta.Data = listaErrores;
                return respuesta;
            }

            DateTimeOffset fechaHoraActual = DateTimeOffset.UtcNow;
            string directorioActual = Directory.GetCurrentDirectory();
            var rutaGuardadoArchivo = directorioActual + "/files";
            var rutaGuardadoImagen = directorioActual + "/images";

            if (!Directory.Exists(rutaGuardadoArchivo))
            {
                Directory.CreateDirectory(rutaGuardadoArchivo);
            }

            if (!Directory.Exists(rutaGuardadoImagen))
                Directory.CreateDirectory(rutaGuardadoImagen);


            var nombreArch = Path.GetFileName(temporalRequest.Archivo.FileName);
            var nombreArchivo = "cv_" + temporalRequest.numero + "_" + fechaHoraActual.ToUnixTimeMilliseconds() + "" + Path.GetExtension(nombreArch);
            var rutaArchivo = Path.Combine(rutaGuardadoArchivo, nombreArchivo);

            var nombreImag = Path.GetFileName(temporalRequest.Imagen.FileName);
            var nombreImagen = "img_" + temporalRequest.numero + "_" + fechaHoraActual.ToUnixTimeMilliseconds() + "" + Path.GetExtension(nombreImag);
            var rutaImagen = Path.Combine(rutaGuardadoImagen, nombreImagen);


            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await temporalRequest.Archivo.CopyToAsync(stream);
            }

            using (var stream = new FileStream(rutaImagen, FileMode.Create))
            {
                await temporalRequest.Imagen.CopyToAsync(stream);
            }

            var postulante = new Postulante();
            postulante.nombre = temporalRequest.nombre;
            postulante.numero = temporalRequest.numero;
            postulante.ape_paterno = temporalRequest.ape_paterno;
            postulante.ape_materno = temporalRequest.ape_materno;
            postulante.celular = temporalRequest.celular;
            postulante.correo = temporalRequest.correo;
            postulante.contrasena = Encrypt.GetSHA256(temporalRequest.contrasena);
            postulante.tipo_id = int.Parse(temporalRequest.tipo_id);
            postulante.fec_nacimiento = DateTime.Parse(temporalRequest.fec_nacimiento);
            postulante.imageurl = nombreImagen;
            postulante.rol_id = 4;
            postulante.seleccion_id = 0;
            postulante.archivocv = nombreArchivo;
            await context.Postulante.AddAsync(postulante);
            context.SaveChanges();

            foreach (int id in temporalRequest.listaEspecialidades)
            {
                var especialidadPostulante = new Especialidad_postulante();
                especialidadPostulante.especialidad_id = id;
                especialidadPostulante.postulante_id = postulante.postulante_id;
                await context.Especialidad_postulante.AddAsync(especialidadPostulante);
                context.SaveChanges();
            }

            respuesta.status = true;
            respuesta.Data = "Usuario creado exitosamente";

            return respuesta;
        }

        [HttpGet]

        public IEnumerable<Postulante> GetLista()
        {

            return context.Postulante.ToList();

        }

        // GET: api/<PostulanteController>
        [HttpGet("listapostulantes")]

        public IEnumerable<Postulante> GetListaPostulante()
        {

            return context.Postulante.Where(p => p.estado_contratado != true).ToList();

        }

        // GET: api/<PostulanteController>
        [HttpGet("listacontradados")]

        public IEnumerable<Postulante> GetListaContratados()
        {
            return context.Postulante.Where(p => p.estado_contratado == true).ToList();
        }

        [HttpGet("{id}")]
        public Postulante Get(int id)
        {
            Postulante postulante = new Postulante();
            postulante = context.Seleccion_cabecera.Join(context.Postulante,
               sd => sd.postulante_id,
               r => r.postulante_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.sd.postulante_id == id)
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
                   contrasena = "",
                   rep_contrasena = "",
                   imageurl = res.r.imageurl,
                   archivocv = res.r.archivocv,
                   rol_id = res.r.rol_id,
                   seleccion_id = res.sd.seleccion_id,
                   correo = res.r.correo,
                   estado = res.r.estado,
                   estado_contratado = res.r.estado_contratado


               }).FirstOrDefault();

            if (postulante == null)
            {
                postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);


            }


            return postulante;
        }


        [HttpGet("GetImagePostulante/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {

            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);

            string path = webHostEnviroment.ContentRootPath + "\\images\\";
            var filePath = path + postulante.imageurl;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            return null;
        }

        [HttpGet("GetArchivoPostulante/{id}")]
        public async Task<IActionResult> GetFile([FromRoute] int id)
        {

            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);

            string path = webHostEnviroment.ContentRootPath + "\\files\\";
            var filePath = path + postulante.archivocv;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "application/pdf");
            }
            return null;
        }




        /*    [HttpPost("AdjuntarImagen/{id}")]
            public ActionResult PostImagen([FromForm] TemporalRequest temporal, int id)
            {
                try
                {
                    TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                    string uniqueFileName = UploadedImagePostulante(temporal);
                    opostulante.archivo = uniqueFileName;
                    opostulante.descripcion = uniqueFileName;
                    opostulante.tipoarchivo = 1;
                    opostulante.modulo = 1;
                    context.TEMPORAL_IMAGEN.Add(opostulante);
                    context.SaveChanges();

                    if (id > 0)
                    {

                        var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);

                        postulante.imageurl = uniqueFileName;
                        context.Entry(postulante).State = EntityState.Modified;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }*/


        /*    [HttpPost("AdjuntarArchivo/{id}")]
            public ActionResult PostARchivo([FromForm] TemporalRequest temporal, int id)
            {
                try
                {
                    TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                    string uniqueFileName = UploadedFilePostulante(temporal);
                    opostulante.archivo = uniqueFileName;
                    opostulante.descripcion = uniqueFileName;
                    opostulante.tipoarchivo = 2;
                    opostulante.modulo = 1;
                    context.TEMPORAL_IMAGEN.Add(opostulante);
                    context.SaveChanges();
                    //return Ok();


                    if (id > 0)
                    {
                        var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);
                        postulante.archivocv = uniqueFileName;
                        context.Entry(postulante).State = EntityState.Modified;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }*/

        [HttpPut("statusPostulante")]
        public ActionResult ActualizarEstado([FromBody] Models.Request.Postulante postulante)
        {
            context4.Entry(postulante).State = EntityState.Modified;
            context4.SaveChanges();

            var result = new OkObjectResult(new { message = "OK", status = true, postulante_id = postulante.postulante_id });
            return result;
        }

        [HttpPut("statusContrado")]
        public ActionResult ActualizarEstadoContratao([FromBody] Models.Request.Postulante postulante)
        {
            var postulanteest = context2.Postulante.FirstOrDefault(p => p.postulante_id == postulante.postulante_id);
            postulanteest.estado_contratado = postulante.estado_contratado;
            context2.Entry(postulanteest).State = EntityState.Modified;
            context2.SaveChanges();

            var result = new OkObjectResult(new { message = "OK", status = true, postulante_id = postulante.postulante_id });
            return result;
        }

        // PUT api/<PostulanteController>/5
        [HttpPut()]
        public ActionResult Put([FromBody] PostulanteRequest postulante)
        {
            //    var temporal_imagen = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 1 & p.modulo == 1);
            //var temporal_archivo = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 2 & p.modulo == 1);
            ////////
            var postulantereque = context.Postulante.FirstOrDefault(p => p.postulante_id == postulante.postulante_id);
            Postulantes opostulante = new Postulantes();
            opostulante.postulante_id = postulante.postulante_id;
            opostulante.nombre = postulante.nombre;
            opostulante.ape_paterno = postulante.ape_paterno;
            opostulante.ape_materno = postulante.ape_materno;
            opostulante.tipo_id = postulante.tipo_id;
            opostulante.numero = postulante.numero;
            opostulante.fec_nacimiento = postulante.fec_nacimiento;
            opostulante.celular = postulante.celular;
            opostulante.correo = postulante.correo;

            if (postulante.contrasena != null)
            {
                opostulante.contrasena = Encrypt.GetSHA256(postulante.contrasena);
            }
            else
            {
                opostulante.contrasena = postulantereque.contrasena;
            }
            if (postulante.contrasena != null)
            {
                opostulante.rep_contrasena = Encrypt.GetSHA256(postulante.rep_contrasena);
            }
            else
            {
                opostulante.rep_contrasena = postulantereque.rep_contrasena;
            }
            opostulante.estado = postulante.estado;
            opostulante.imageurl = postulante.imageurl;
            opostulante.archivocv = postulante.archivocv;
            opostulante.rol_id = postulante.rol_id;
            opostulante.seleccion_id = postulante.seleccion_id;
            context2.Entry(opostulante).State = EntityState.Modified;
            context2.SaveChanges();

            if (postulante.Especialidades != null)

            {

                var especialidad = context.Especialidad_postulante.ToList().Where((c => c.postulante_id == postulante.postulante_id));

                Especialidad_postulante oespecialidad = new Especialidad_postulante();

                if (especialidad != null)
                {

                    foreach (var especialidadespostulante in especialidad)
                    {

                        var especialidadespecial = context.Especialidad_postulante.FirstOrDefault(p => p.especialidad_post_id == especialidadespostulante.especialidad_post_id);
                        //oespecialidad.especialidad_post_id = especialidadespostulante.especialidad_post_id;
                        context.Especialidad_postulante.Remove(especialidadespecial);
                        context.SaveChanges();
                    }

                    foreach (var oPostEspecialidade in postulante.Especialidades)
                    {

                        oespecialidad.especialidad_post_id = 0;
                        oespecialidad.postulante_id = postulante.postulante_id;
                        oespecialidad.especialidad_id = oPostEspecialidade.especialidad_id;
                        context.Especialidad_postulante.Add(oespecialidad);
                        context.SaveChanges();
                    }


                }

            }
            var result = new OkObjectResult(new { message = "OK", status = true, postulante_id = postulante.postulante_id });
            return result;
            //if (postulante.postulante_id == id)
            //{
            //    context.Entry(postulante).State = EntityState.Modified;
            //    context.SaveChanges();
            //    return Ok();
            //}
            //else
            //{
            //    return BadRequest();
            //}
        }


        [HttpPost("NotificacionRegistro")]
        public string Notificar([FromBody] NOTIFICACION notificacion)
        {
            var vnotificacion = context.ENVIAR_CORREO.FirstOrDefault(p => p.envio_id == 2);
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

        /*   private string UploadedImagePostulante(TemporalRequest temporal)
           {
               string uniqueFileName = null;
               if (temporal.FrontArchivo != null)
               {
                   string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images");
                   uniqueFileName = temporal.FrontArchivo.FileName;
                   string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                   using (var fileStream = new FileStream(filePath, FileMode.Create))
                   {
                       temporal.FrontArchivo.CopyTo(fileStream);
                   }

               }
               return uniqueFileName;
           }*/

        /*  private string UploadedFilePostulante(TemporalRequest temporal)
          {
              string uniqueFileName = null;
              if (temporal.FrontArchivo != null)
              {
                  string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "files");
                  uniqueFileName =  temporal.FrontArchivo.FileName;
                  string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                  using (var fileStream = new FileStream(filePath, FileMode.Create))
                  {
                      temporal.FrontArchivo.CopyTo(fileStream);
                  }

              }
              return uniqueFileName;
          }*/

        private bool Validarcorreo(string correo, List<Postulante> postulantes)
        {

            bool validarcorreo = true;
            foreach (var oPostpostulantelist in postulantes)
            {
                if (oPostpostulantelist.correo == correo)

                {
                    validarcorreo = false;

                    return false;

                }
            }
            return validarcorreo;
        }


        private bool Validarnumero(string numero, List<Postulante> postulantes)
        {

            bool validarnumero = true;
            foreach (var oPostpostulantelist in postulantes)
            {
                if (oPostpostulantelist.numero == numero)

                {
                    validarnumero = false;

                    return false;

                }
            }
            return validarnumero;
        }

        // DELETE api/<PostulanteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);
            if (postulante != null)
            {
                context.Postulante.Remove(postulante);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("detalle/{id}")]
       // [Authorize]
        public async Task<Respuesta> GetDetallePostulante([FromRoute] int id)
        {
            var response = new Respuesta();
            response.status = true;
            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);

            if (postulante == null)
            {
                response.status = false;
                response.Data = "El ID del postulante no existe";
            }
            else
            {
                var seleccion = context.Seleccion_cabecera.FirstOrDefault(s => s.postulante_id == id);
                var postulanteDetalle = new PostulanteDetalle();
                postulanteDetalle.IdPostulante = id;
                postulanteDetalle.NombrePostulante = postulante.nombre;
                postulanteDetalle.ApellidoPostulante = postulante.ape_paterno + " " + postulante.ape_materno;
                if (seleccion != null)
                {
                    postulanteDetalle.FlagSeleccion = true;
                    postulanteDetalle.IdSeleccion = seleccion.area_id;
                    postulanteDetalle.IdSemestre = seleccion.semestre_id;
                    postulanteDetalle.IdPreguntaSeleccion = postulante.seleccion_id;


                }
                else
                {
                    postulanteDetalle.FlagSeleccion = false;
                }

                var listaEspecialidadPostulanteInt = context.Especialidad_postulante.Where(e => e.postulante_id == id).ToList();
                var listaEspecialidadPostulante = new List<Especialidad>();
                foreach (Especialidad_postulante especialidadPost in listaEspecialidadPostulanteInt)
                {
                    var especialidad = context.Especialidad.FirstOrDefault(e => e.especialidad_id == especialidadPost.especialidad_id && e.estado == true);
                    if (especialidad != null) listaEspecialidadPostulante.Add(especialidad);
                }
                postulanteDetalle.ListaEspecialidadesPostulante = listaEspecialidadPostulante;
                postulanteDetalle.ListaSemestre = context.Semestre.ToList();

                //var listaEspecialidades = context.Especialidad.Where(e => e.estado == true);
                var SelectListaEspecialidades = new List<DetalleEspecialidades>();

                foreach (Especialidad esp in listaEspecialidadPostulante)
                {
                    var cursos = context.Especialidad_cursos.Where(c => c.Especialidad_id == esp.especialidad_id).ToList();
                    cursos.Sort((c1, c2) => c1.Nombre.CompareTo(c2.Nombre));
                    var DetalleEspecialidades = new DetalleEspecialidades();
                    DetalleEspecialidades.Especialidad = esp;
                    DetalleEspecialidades.Cursos = cursos;
                    SelectListaEspecialidades.Add(DetalleEspecialidades);
                }
                postulanteDetalle.SelectListaEspecialidades = SelectListaEspecialidades;
                response.Data = postulanteDetalle;
            }



            return response;
        }

        [HttpGet("lista/semestre/{id}")]
        public async Task<Respuesta> GetListaPostulanteBySemestre([FromRoute] int id)
        {
            var respuesta = new Respuesta();
            respuesta.status = false;
            var semestresTemp = context.Semestre.FirstOrDefault(s => s.semestre_id == id);
            if (semestresTemp == null)
            {
                respuesta.Data = "El semestre no existe";
                return respuesta;
            }

            var sql = from p in context.Postulante
                      join s in context.Seleccion_cabecera on p.postulante_id equals s.postulante_id
                      //join e in context.DETALLE_EVALUACION on p.postulante_id equals e.POSTULANTE_ID
                      where p.estado_contratado == true && s.semestre_id.Equals(id)
                      select new
                      {
                          postulante_id = p.postulante_id,
                          nombre = p.nombre,
                          ape_paterno = p.ape_paterno,
                          ape_materno = p.ape_materno,
                          correo = p.correo,
                          seleccion_id = s.seleccion_id,
                          rol_id = p.rol_id,
                          estado_contratado = p.estado_contratado,
                      };
            var listaPostulantes = new List<PostulanteEvaluacion>();
            foreach (var item in sql)
            {
                var evaluacion = context.DETALLE_EVALUACION.FirstOrDefault(d => d.POSTULANTE_ID == item.postulante_id);
                var postulante = new PostulanteEvaluacion();
                postulante.postulante_id = item.postulante_id;
                postulante.nombre = item.nombre;
                postulante.ape_paterno = item.ape_paterno;
                postulante.ape_materno = item.ape_materno;
                postulante.correo = item.correo;
                postulante.seleccion_id = item.seleccion_id;
                postulante.rol_id = item.rol_id;
                postulante.estado_contratado = item.estado_contratado;

                if (evaluacion != null)
                {
                    postulante.flagTipo = "" + evaluacion.ESTADO;
                    postulante.detalle_evaluacion_id = evaluacion.DETALLE_EVALUACION_ID;
                    postulante.enc_estu = evaluacion.ENC_ESTU;
                    postulante.cum_adm = evaluacion.CUM_ADM;
                    postulante.acom_doc = evaluacion.ACOM_DOC;
                    postulante.cap_doc = evaluacion.CAP_DOC;
                    postulante.cum_vir = evaluacion.CUM_VIR;
                    postulante.nota_final = evaluacion.NOTA_FINAL;
                }
                listaPostulantes.Add(postulante);
            }

            respuesta.status = true;
            respuesta.Data = listaPostulantes;

            return respuesta;
        }

        [HttpPost("recuperarClave")]
        public Respuesta RecuperarClave([FromBody] RecuperarClaveRequest recuperarClaveRequest)
        {
            var respuesta = new Respuesta();
            respuesta.status = false;
            var postulante = context.Postulante.FirstOrDefault(p => p.correo == recuperarClaveRequest.email);
            if (postulante == null)
            {
                respuesta.Data = "No existe el email";
                return respuesta;
            }

            string codigo = Guid.NewGuid().ToString("N");


            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("ronald.livia@outlook.com"));
            email.To.Add(MailboxAddress.Parse(postulante.correo));
            email.Subject = "ADEX: Recuparación de contraseña";
            var url = recuperarClaveRequest.ruta + "?code=" + codigo + "&id=" + postulante.postulante_id;
            var html = "<p>      Estimado/a postulante,    </p>    " +
                "<p>      De nuestra mayor consideración,    </p>   " +
                " <p>      Desde ya, esperamos que se encuentre bien de salud al igual que sus seres queridos.     </p>   " +
                " <p> <b>     Usted ha solicitado una recuperación de su contraseña y para ello le enviamos el siguiente link donde podrá hacer el cambio.   </b> </p> <br/>" +
                "<a href='" + url + "' >" + url + "</a> " +
                "  <p>      Si usted no ha solicitado una recuperación de contraseña, comuníquese inmediatamente a 941488793";
            postulante.rep_contrasena = codigo;
            context.Entry(postulante).State = EntityState.Modified;
            context.SaveChanges();
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(Settings.smtp, Settings.puerto, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(Settings.email, Settings.password);
            smtp.Send(email);
            smtp.Disconnect(true);
            respuesta.status = true;

            return respuesta;
        }

        [HttpPost("recuperarClave/verificar")]
        public Respuesta verificarRecuperarClave([FromBody] VerificarRecuperarClaveRequest v)
        {
            var respuesta = new Respuesta();
            respuesta.status = false;
            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == v.IdPostulante && p.rep_contrasena == v.Codigo);
            if (postulante == null)
            {
                respuesta.Data = "El código ha expirado.";
                return respuesta;
            }

            postulante.contrasena = Encrypt.GetSHA256(v.Clave);
            postulante.rep_contrasena = null;
            context.Entry(postulante).State = EntityState.Modified;
            context.SaveChanges();
            respuesta.status= true;
            respuesta.Data= "Se ha realizado el cambio de contraseña.";
            return respuesta;
        }
    }
}
