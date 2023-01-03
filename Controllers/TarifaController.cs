using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TarifaController : ControllerBase
    {
        private readonly AppDbContext context;

        public TarifaController(AppDbContext context)
      
        {
            this.context = context;
        }


        // GET: api/<TarifaController>
        [HttpGet]
        public IEnumerable<Tarifa> Get()
        {

            return context.Tarifa.ToList();

        }

        // GET api/<TarifaController>/5
        [HttpGet("{id}")]
        public Tarifa Get(int id)
        {
            var semestre = context.Tarifa.FirstOrDefault(p => p.tarifa_id == id);
            return semestre;
        }

        // POST api/<TarifaController>
        [HttpPost]
        public ActionResult Post([FromBody] Tarifa tarifa)
        {
            try
            {
                context.Tarifa.Add(tarifa);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<TarifaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tarifa tarifa)
        {
            if (tarifa.tarifa_id == id)
            {
                context.Entry(tarifa).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<TarifaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var tarifa = context.Tarifa.FirstOrDefault(p => p.tarifa_id == id);
            if (tarifa != null)
            {
                context.Tarifa.Remove(tarifa);
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
