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
    public class RevisionCVController : ControllerBase
    {
        private readonly AppDbContext context;
        public RevisionCVController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<RevisionCVController>
        [HttpGet]
        public IEnumerable<Revision> Get()
        {

            return context.RevisionCV.ToList();

        }

        // GET api/<RevisionCVController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RevisionCVController>
        [HttpPost]
        public ActionResult GrabarRevision([FromBody] Revision revision)
        {
            try
            {
                context.RevisionCV.Add(revision);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        // PUT api/<RevisionCVController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RevisionCVController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
