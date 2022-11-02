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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulanteController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<PostulanteController> logger;
        private readonly IWebHostEnvironment webHostEnviroment;
        public PostulanteController(AppDbContext context, ILogger<PostulanteController> logger, IWebHostEnvironment webHost)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
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

            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);
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
            try
            {

                var temporal_imagen = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 1 & p.modulo == 1);

                var temporal_archivo = context.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 2 & p.modulo == 1);

                Postulante opostulante = new Postulante();
                opostulante.nombre = postulante.nombre;
                opostulante.ape_paterno = postulante.ape_paterno;
                opostulante.ape_materno = postulante.ape_materno;
                opostulante.dni = postulante.dni;
                opostulante.fec_nacimiento = postulante.fec_nacimiento;
                opostulante.celular = postulante.celular;
                opostulante.correo = postulante.correo;
                opostulante.contrasena = postulante.contrasena;
                opostulante.rep_contrasena = postulante.rep_contrasena;

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

                return Ok("Success");

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        [HttpPost("AdjuntarImagen/{id}")]
        public ActionResult PostImagen([FromForm] TemporalRequest temporal)
        {
            try
            {
                TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                string uniqueFileName = UploadedFilePostulante(temporal);
                opostulante.archivo = uniqueFileName;
                opostulante.descripcion = uniqueFileName;
                opostulante.tipoarchivo = 1;
                opostulante.modulo = 1;
                context.TEMPORAL_IMAGEN.Add(opostulante);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost("AdjuntarArchivo/{id}")]
        public ActionResult PostARchivo([FromForm] TemporalRequest temporal)
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        // PUT api/<PostulanteController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Postulante postulante)
        {
            if (postulante.postulante_id == id)
            {
                context.Entry(postulante).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //private string UploadedFileImage(Postulante postulante)
        //{
        //    string uniqueFileName = null;
        //    if (postulante.celular != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + postulante.FrontImage.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            postulante.FrontImage.CopyTo(fileStream);
        //        }

        //    }
        //    return uniqueFileName;
        //}

        //private string UploadedFile(Postulante postulante)
        //{
        //    string uniqueFileName = null;
        //    if (postulante.celular != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "files");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + postulante.FrontArchivo.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            postulante.FrontImage.CopyTo(fileStream);
        //        }

        //    }
        //    return uniqueFileName;
        //}


        private string UploadedFilePostulante(TemporalRequest temporal)
        {
            string uniqueFileName = null;
            if (temporal.FrontArchivo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images");
                uniqueFileName =  temporal.FrontArchivo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    temporal.FrontArchivo.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
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
