using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using System.Collections.Generic;
using SIGED_API.Entity;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/rol")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly AppDbContext context;

        public RolController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<RolController>/5
        [HttpGet]
        public IEnumerable<ROL> Get()
        {

            return context.ROL.ToList();

        }

        // POST api/<RolController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RolController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RolController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
