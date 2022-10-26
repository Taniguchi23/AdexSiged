using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PregradoController : ControllerBase
    {
        private readonly AppDbContext2 context;

        public PregradoController(AppDbContext2 context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<PregradoController>
        [HttpGet]
        public IEnumerable<Pregrado> Get()
        {

            return context.Pregrado.ToList();

        }
        // GET api/<PregradoController>/5
        [HttpGet("{id}")]
        public Pregrado Get(int id)
        {
            var pregrado = context.Pregrado.FirstOrDefault(p => p.pregrado_id == id);
            return pregrado;
        }

        // POST api/<PregradoController>
        [HttpPost]
        public ActionResult Post([FromBody] Pregrado pregrado)
        {
            try
            {
                context.Pregrado.Add(pregrado);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<PregradoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Pregrado pregrado)
        {
            if (pregrado.pregrado_id == id)
            {
                context.Entry(pregrado).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<PregradoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var pregrado = context.Pregrado.FirstOrDefault(p => p.pregrado_id == id);
            if (pregrado != null)
            {
                context.Pregrado.Remove(pregrado);
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
