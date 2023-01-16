using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/reporte")]
    [ApiController]
    [Authorize]
    public class ReporteController : ControllerBase
    {

        private readonly AppDbContext context;

        public ReporteController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        //// GET: api/<ReporteController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ReporteController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ReporteController>
        [HttpPost]
        public int GrabarReporte([FromBody] REPORTE reporte)
        {

            var vreporte = context.REPORTE.FirstOrDefault(p => p.postulante_id == reporte.postulante_id & p.area_id == reporte.area_id);

            if (vreporte != null)
            {
                return vreporte.reporte_id;
            }
            else
            {
                REPORTE oreporte = new REPORTE();
                oreporte.fecha = reporte.fecha;
                oreporte.postulante_id = reporte.postulante_id;
                oreporte.evaluador_id = reporte.evaluador_id;
                oreporte.area_id = reporte.area_id;
                context.REPORTE.Add(oreporte);
                context.SaveChanges();

                return oreporte.reporte_id;
            }


        }

        // PUT api/<ReporteController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReporteController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
