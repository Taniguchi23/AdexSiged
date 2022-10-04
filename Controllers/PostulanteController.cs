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

        // GET api/<PostulanteController>/5

        //public PostulanteInformacion GetPostulanteInformacion(int id)
        //{

        //    var postulante = context.Postulante.
        //        Join(context.Area_interes,
        //        p => p.postulante_id,
        //        ai => ai.postulante_id,
        //        (p, ai) => new { p, ai }
        //        )
        //        .Join(context.Area,
        //        a => a.ai.area_id,
        //        ai => ai.area_id,
        //        (a, ai) => new { a, ai }
        //        ).Where(c => c.a.p.postulante_id == id)
        //        .Select(res => new PostulanteInformacion()
        //        {
        //            postulante_id = res.a.p.postulante_id,
        //            nombre = res.a.p.nombre,
        //            ape_materno = res.a.p.ape_materno,
        //            ape_paterno = res.a.p.ape_paterno,
        //            dni = res.a.p.dni,
        //            fec_nacimiento = res.a.p.fec_nacimiento,
        //            celular = res.a.p.celular,
        //            correo = res.a.p.correo,
        //            contrasena = res.a.p.contrasena,
        //            rep_contrasena = res.a.p.rep_contrasena,
        //            area = res.ai.area,
        //            area_id = res.ai.area_id,

        //        }).FirstOrDefault<PostulanteInformacion>();

        //    return postulante;

        //}

        //public IEnumerable<PostulanteInformacion> GetPersonalInformacion(int id)
        //{

        //    var postulante = context.Postulante.
        //        Join(context.Area_interes,
        //        p => p.postulante_id,
        //        ai => ai.postulante_id,
        //        (p, ai) => new { p, ai }
        //        )
        //        .Join(context.Area,
        //        a => a.ai.area_id,
        //        ai => ai.area_id,
        //        (a, ai) => new { a, ai }
        //        ).Where(c => c.a.p.postulante_id == id)
        //        .Select(res => new PostulanteInformacion()
        //        {
        //            nombre = res.a.p.nombre,

        //        }).ToList();


        //    return postulante;

        //}
        [HttpGet("{id}")]
        public Postulante Get(int id)
        {

            var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);
            return postulante;
        }

        // POST api/<PostulanteController>
        [HttpPost]
        public async Task<string> Post([FromBody] Postulante postulante)
        {
            try
            {
                string uniqueFileName = UploadedFile(postulante);
                postulante.imageurl = uniqueFileName;
                context.Postulante.Add(postulante);
                context.SaveChanges();
                return "OK";
                
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        //public ActionResult Post([FromBody] Postulante postulante)
        //{
        //    try
        //    {
        //        string uniqueFileName = UploadedFile(postulante);
        //        postulante.imageurl = uniqueFileName;
        //        context.Postulante.Add(postulante);
        //        context.SaveChanges();
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest();
        //    }       
        //}


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

        private string UploadedFile(Postulante postulante)
        {
            string uniqueFileName = null;
            if (postulante.celular != null)
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + postulante.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    postulante.FrontImage.CopyTo(fileStream);
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
