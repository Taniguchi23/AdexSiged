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
    public class PostgradoController : ControllerBase
    {
        private readonly AppDbContext2 context;

        public PostgradoController(AppDbContext2 context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<PostgradoController>
        [HttpGet]
        public IEnumerable<Postgrado> Get()
        {

            return context.Postgrado.ToList();

        }

        // GET api/<PostgradoController>/5
        [HttpGet("{id}")]
        public Postgrado Get(int id)
        {
            var postgrado = context.Postgrado.FirstOrDefault(p => p.postgrado_id == id);
            return postgrado;
        }

        // POST api/<PostgradoController>
        [HttpPost]
        public ActionResult Post([FromBody] Postgrado postgrado)
        {
            try
            {
                context.Postgrado.Add(postgrado);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // PUT api/<PostgradoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Postgrado postgrado)
        {
            if (postgrado.postgrado_id == id)
            {
                context.Entry(postgrado).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<PostgradoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var postgrado = context.Postgrado.FirstOrDefault(p => p.postgrado_id == id);
            if (postgrado != null)
            {
                context.Postgrado.Remove(postgrado);
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
