using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisionCVController : ControllerBase
    {
        private readonly AppDbContext context;
        public RevisionCVController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<RevisionCVController>
        [HttpGet]
        public IEnumerable<Revision> Get()
        {

            return context.RevisionCV.ToList();

        }

        // GET api/<RevisionCVController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RevisionCVController>
        [HttpPost]
        public ActionResult GrabarRevision([FromBody] RevisionRequest revision)
        {
            try
            {
                Revision orevision = new Revision();
                orevision.fecha_rev = revision.fecha_rev;
                orevision.seleccion_c1 = revision.seleccion_c1;
                orevision.seleccion_c2 = revision.seleccion_c2;
                orevision.seleccion_c3 = revision.seleccion_c3;
                orevision.seleccion_c4 = revision.seleccion_c4;
                orevision.seleccion_c5 = revision.seleccion_c5;
                orevision.seleccion_c6 = revision.seleccion_c6;
                orevision.comentario_1 = revision.comentario_1;
                orevision.comentario_2 = revision.comentario_2;
                orevision.comentario_3 = revision.comentario_3;
                orevision.comentario_4 = revision.comentario_4;
                orevision.comentario_5 = revision.comentario_5;
                orevision.comentario_6 = revision.comentario_6;
                orevision.observacion = revision.observacion;
                orevision.estado = revision.estado;
                context.RevisionCV.Add(orevision);
                context.SaveChanges();

                Seleccion_detalle oseleccion_detalle = new Seleccion_detalle();
                oseleccion_detalle.seleccion_id = revision.seleccion_id;
                oseleccion_detalle.revision_id = orevision.revision_id;
                context.Seleccion_detalle.Add(oseleccion_detalle);
                context.SaveChanges();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        // PUT api/<RevisionCVController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RevisionCVController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
