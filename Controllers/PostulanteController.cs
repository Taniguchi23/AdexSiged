using Microsoft.AspNetCore.Mvc;
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
    public class PostulanteController : ControllerBase
    {
        private readonly AppDbContext context;
        public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<PostulanteController>
        [HttpGet]
        public IEnumerable<Postulante> Get()
        {
            return context.Postulante.ToList();
        }



        // GET api/<PostulanteController>/5
        [HttpGet("{id}")]
        public Postulante Get(int id)
        {

            var postulante = context.Postulante.FirstOrDefault(p => p.POSTULANTE_ID == id);
            return postulante;
        }

        // POST api/<PostulanteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostulanteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostulanteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
