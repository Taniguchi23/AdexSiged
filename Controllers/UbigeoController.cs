using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UbigeoController : ControllerBase
    {
        private readonly AppDbContext context;

        public UbigeoController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<UbigeoController>
        [HttpGet("Departamento")]
        public IEnumerable<DEPARTAMENTO> GetDepartamento()
        {
            return context.DEPARTAMENTO.ToList();
        }

        // GET api/<UbigeoController>/5
        [HttpGet("Provincia/{DEPARTAMENTO_ID}")]
        public IEnumerable<PROVINCIA> GetProvincia(int DEPARTAMENTO_ID)
        {
            return context.PROVINCIA.Where((p => p.DEPARTAMENTO_ID == DEPARTAMENTO_ID)).ToList();
        }


        // GET api/<UbigeoController>/5
        [HttpGet("Distrito/{PROVINCIA_ID}")]
        public IEnumerable<DISTRITO> GetDistrito(int PROVINCIA_ID)
        {
            return context.DISTRITO.Where((p => p.PROVINCIA_ID == PROVINCIA_ID)).ToList();
        }

        // POST api/<UbigeoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UbigeoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UbigeoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
