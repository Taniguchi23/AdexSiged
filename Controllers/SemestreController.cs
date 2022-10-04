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
    [Route("api/[controller]")]
    [ApiController]
    public class SemestreController : ControllerBase
    {
        private readonly AppDbContext context;

        public SemestreController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }


        // GET: api/<SemestreController>
        [HttpGet]
        public IEnumerable<Semestre> Get()
        {

            return context.Semestre.ToList();

        }

        // GET api/<SemestreController>/5
        [HttpGet("{id}")]
        public Semestre Get(int id)
        {
            var semestre = context.Semestre.FirstOrDefault(p => p.semestre_id == id);
            return semestre;
        }

        // POST api/<SemestreController>
        [HttpPost]
        public ActionResult Post([FromBody] Semestre semestre)
        {
            try
            {
                context.Semestre.Add(semestre);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<SemestreController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Semestre semestre)
        {
            if (semestre.semestre_id == id)
            {
                context.Entry(semestre).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<SemestreController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var semestre = context.Semestre.FirstOrDefault(p => p.semestre_id == id);
            if (semestre != null)
            {
                context.Semestre.Remove(semestre);
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
