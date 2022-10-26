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
    public class RutinaController : ControllerBase
    {
        private readonly AppDbContext context;

        public RutinaController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<RutinaController>
        [HttpGet]
        public IEnumerable<RUTINA> Get()
        {

            return context.RUTINA.ToList();

        }

        // GET api/<RutinaController>/5
        [HttpGet("api/RutinaGrupo1")]
        public IEnumerable<RUTINA> GetRutinaGrupo1()
        {

            var parametro = context.RUTINA.Where(p => p.GRUPO == 1).ToList();

            return parametro;
        }

        [HttpGet("api/RutinaGrupo2")]
        public IEnumerable<RUTINA> GetRutinaGrupo2()
        {

            var parametro = context.RUTINA.Where(p => p.GRUPO == 2).ToList();

            return parametro;
        }

        [HttpGet("api/RutinaGrupo3")]
        public IEnumerable<RUTINA> GetRutinaGrupo3()
        {

            var parametro = context.RUTINA.Where(p => p.GRUPO == 3).ToList();

            return parametro;
        }

        [HttpGet("api/RutinaGrupo4")]
        public IEnumerable<RUTINA> GetRutinaGrupo4()
        {

            var parametro = context.RUTINA.Where(p => p.GRUPO == 4).ToList();

            return parametro;
        }

        // POST api/<RutinaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RutinaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RutinaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
