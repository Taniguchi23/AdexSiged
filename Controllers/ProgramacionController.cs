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
    public class ProgramacionController : ControllerBase
    {

        private readonly AppDbContext2 context;

        public ProgramacionController(AppDbContext2 context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ProgramacionController>
        [HttpGet]
        public IEnumerable<PROGRAMACION> Get()
        {

            return context.PROGRAMACION.ToList();

        }

        // GET api/<ProgramacionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProgramacionController>
        [HttpPost]
        public int GrabarReporte([FromBody] PROGRAMACION programacion)
        {

            var vnotificacion = context.PROGRAMACION.FirstOrDefault(p => p.postulante_id == programacion.postulante_id);

            if (vnotificacion != null)
            {
                return vnotificacion.programacion_id;
            }
            else
            {
                PROGRAMACION oreporte = new PROGRAMACION();
                oreporte.fecha = programacion.fecha;
                oreporte.postulante_id = programacion.postulante_id;
                oreporte.fecha = programacion.fecha;
                oreporte.estado = programacion.estado;
                context.PROGRAMACION.Add(oreporte);
                context.SaveChanges();

                return oreporte.programacion_id;
            }


        }

        // PUT api/<ProgramacionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProgramacionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
