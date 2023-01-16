using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/especialidad")]
    [ApiController]
    //[Authorize]
    public class EspecialidadController : ControllerBase
    {
        private readonly AppDbContext context;

        public EspecialidadController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<EspecialidadController>
        [HttpGet]
        public IEnumerable<Especialidad> Get()
        {

            return context.Especialidad.ToList();

        }

        // GET api/<EspecialidadController>/5
        [HttpGet("{id}")]
        public Especialidad Get(int id)
        {

            var especialidad = context.Especialidad.FirstOrDefault(p => p.especialidad_id == id);
            return especialidad;
        }

        [HttpGet("GetEspecialidadbyPostulante/{id}")]
        public IEnumerable<Especialidad> GetEspecialidadbyPostulante(int id)
        {

            var postulante = context.Postulante.
                Join(context.Especialidad_postulante,
                p => p.postulante_id,
                ai => ai.postulante_id,
                (p, ai) => new { p, ai }
                )
                .Join(context.Especialidad,
                a => a.ai.especialidad_id,
                ai => ai.especialidad_id,
                (a, ai) => new { a, ai }
                ).Where(c => c.a.p.postulante_id == id)
                .Select(res => new Especialidad()
                {
                    especialidad_id = res.ai.especialidad_id,
                    nombre = res.ai.nombre,
                    descripcion = res.ai.descripcion,
                    estado = res.ai.estado


                }).ToList();

            return postulante;

        }

        // POST api/<EspecialidadController>
        [HttpPost]
        public ActionResult Post([FromBody] Especialidad especialidad)
        {

            try
            {
                context.Especialidad.Add(especialidad);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // PUT api/<EspecialidadController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Especialidad especialidad)
        {

            if (especialidad.especialidad_id == id)
            {
                context.Entry(especialidad).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<EspecialidadController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            var especialidad = context.Especialidad.FirstOrDefault(p => p.especialidad_id == id);
            if (especialidad != null)
            {
                context.Especialidad.Remove(especialidad);
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
