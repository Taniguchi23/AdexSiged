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
using System.Web;
using Microsoft.AspNetCore.Hosting;


using System.Threading.Tasks;
using SIGED_API.Models;
using Newtonsoft.Json;
using Postulante = SIGED_API.Entity.Postulante;
using Postulantes = SIGED_API.Models.Postulante;
using Microsoft.AspNetCore.Authorization;
using SIGED_API.Tools;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/postulante")]
    [ApiController]
    //[Authorize]
    public class PostulanteController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly AppDbContext2 context2;
        private readonly ILogger<PostulanteController> logger;
        private readonly IWebHostEnvironment webHostEnviroment;
        public PostulanteController(AppDbContext context, AppDbContext2 context2, ILogger<PostulanteController> logger, IWebHostEnvironment webHost)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
            this.context2 = context2;
            webHostEnviroment = webHost;
        }

        // GET: api/<PostulanteController>
        [HttpGet]

        public IEnumerable<Postulante> Get()
        {

            try
            {
                return context.Postulante.ToList();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error" + ex.Message);
                throw;
            }
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
                   contrasena = Encrypt.GetSHA256(res.r.contrasena),
                   rep_contrasena = Encrypt.GetSHA256(res.r.rep_contrasena),
                   imageurl = res.r.imageurl,
                   archivocv = res.r.archivocv,
                   rol_id = res.r.rol_id,
                   seleccion_id = res.sd.seleccion_id,
                   correo= res.r.correo,
                   estado = res.r.estado

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


        [HttpPost]
        public ActionResult Post([FromBody] PostulanteRequest postulante)
        {
            var result = new OkObjectResult(0);
            try
            {

                List<Postulante> postulanteList = new List<Postulante>();

                postulanteList = context.Postulante.ToList();

                bool valorcorreo, valornumero;


                valorcorreo = Validarcorreo(postulante.correo, postulanteList);
                valornumero = Validarnumero(postulante.numero, postulanteList);


                if (valorcorreo == false)
                {

                    result = new OkObjectResult(new { message = "Ya existe correo", status = false });

                }

                else if (valornumero == false)
                {

                    result = new OkObjectResult(new { message = "Ya existe numero", status = false });

                }

                else
                {

                    var temporal_imagen = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 1 & p.modulo == 1);

                var temporal_archivo = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 2 & p.modulo == 1);

                Postulante opostulante = new Postulante();
                opostulante.nombre = postulante.nombre;
                opostulante.ape_paterno = postulante.ape_paterno;
                opostulante.ape_materno = postulante.ape_materno;
                opostulante.tipo_id = postulante.tipo_id;
                opostulante.numero = postulante.numero;
                opostulante.fec_nacimiento = postulante.fec_nacimiento;
                opostulante.celular = postulante.celular;
                opostulante.correo = postulante.correo;
                opostulante.contrasena = Encrypt.GetSHA256(postulante.contrasena);
                opostulante.rep_contrasena = Encrypt.GetSHA256(postulante.rep_contrasena);
                opostulante.rol_id = 4;

                if (temporal_imagen != null)
                {

                    opostulante.imageurl = temporal_imagen.archivo;
                    context.TEMPORAL_IMAGEN.Remove(temporal_imagen);
                    context.SaveChanges();

                }
                if (temporal_archivo != null)
                {
                    opostulante.archivocv = temporal_imagen.archivo;
                    context.TEMPORAL_IMAGEN.Remove(temporal_archivo);
                    context.SaveChanges();

                }
               

                if (postulante.postulante_id != 0)
                {
                    opostulante.postulante_id = postulante.postulante_id;
                    context.Entry(opostulante).State = EntityState.Modified;
                    context.SaveChanges();

                }
                else
                {
                    context.Postulante.Add(opostulante);
                    context.SaveChanges();

                }

                foreach (var oPostEspecialidad in postulante.Especialidades)
                {
                    var especialidad = context.Especialidad_postulante.FirstOrDefault(p =>  p.postulante_id == postulante.postulante_id & p.especialidad_id == oPostEspecialidad.especialidad_id) ;

                    Especialidad_postulante oespecialidad = new Especialidad_postulante();

                    if (especialidad != null)
                        {
                            oespecialidad.especialidad_post_id = especialidad.especialidad_post_id;
                            context.Especialidad_postulante.Remove(especialidad);
                            context.SaveChanges(); 
                        }

                        oespecialidad.especialidad_post_id = 0;
                        oespecialidad.postulante_id = opostulante.postulante_id;
                        oespecialidad.especialidad_id = oPostEspecialidad.especialidad_id;
                        context.Especialidad_postulante.Add(oespecialidad);
                        context.SaveChanges();
                    }

                    result = new OkObjectResult(new { message = "OK", status = true, postulante_id = opostulante.postulante_id });

                }
                return result;

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("AdjuntarImagen/{id}")]
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
        }


        [HttpPost("AdjuntarArchivo/{id}")]
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
        }



        // PUT api/<PostulanteController>/5
        [HttpPut()]
        public ActionResult Put([FromBody] PostulanteRequest postulante)
        {



            //    var temporal_imagen = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 1 & p.modulo == 1);

            //var temporal_archivo = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 2 & p.modulo == 1);


            ////////
            //var postulantereque = context.Postulante.FirstOrDefault(p => p.postulante_id == postulante.postulante_id);

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
            opostulante.contrasena = Encrypt.GetSHA256(postulante.contrasena);
            opostulante.rep_contrasena = Encrypt.GetSHA256(postulante.rep_contrasena);
            opostulante.estado = postulante.estado;
            opostulante.imageurl = postulante.imageurl;
            opostulante.archivocv = postulante.archivocv;
            opostulante.rol_id = postulante.rol_id;
            opostulante.seleccion_id = postulante.seleccion_id;
            context2.Entry(opostulante).State = EntityState.Modified;
            context2.SaveChanges();

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

        private string UploadedImagePostulante(TemporalRequest temporal)
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
        }

        private string UploadedFilePostulante(TemporalRequest temporal)
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
        }

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
    }
}
