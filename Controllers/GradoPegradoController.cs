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
    public class GradoPegradoController : ControllerBase
    {
        private readonly AppDbContext context;

        public GradoPegradoController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<GradoPegradoController>
        [HttpGet]
        public IEnumerable<GRADOACAD_PREGRADO> Get()
        {

            return context.GRADOACAD_PREGRADO.ToList();

        }

        // GET api/<GradoPegradoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<GradoPegradoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<GradoPegradoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<GradoPegradoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
