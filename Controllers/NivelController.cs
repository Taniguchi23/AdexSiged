using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelController : ControllerBase
    {
        private readonly AppDbContext context;

        public NivelController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }


        // GET: api/<NivelController>
        [HttpGet]
        public IEnumerable<Nivel> Get()
        {

            return context.Nivel.ToList();

        }

        // GET api/<NivelController>/5
        [HttpGet("{id}")]
        public Nivel Get(int id)
        {
            var area = context.Nivel.FirstOrDefault(p => p.id_valoracion == id);
            return area;
        }

        // POST api/<NivelController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<NivelController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<NivelController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
