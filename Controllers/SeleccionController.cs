using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleccionController : ControllerBase
    {
        private readonly AppDbContext context;
        int Ecompetemcia_n2 = Convert.ToInt32(ConfigurationManager.AppSettings["Ecompetemcia_n2"]);
        int Etecnica_n2 = Convert.ToInt32(ConfigurationManager.AppSettings["Etecnica_n2"]);
        public SeleccionController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<SeleccionController>
        [HttpGet]
        public IEnumerable<Seleccion> Get()
        {

            return context.Seleccion_cabecera.ToList();

        }

        // GET: api/<SeleccionController>
        [HttpGet("api/PreguntasRevision")]
        public IEnumerable<Parametro> GetPreguntasRevision()
        {

            var parametro = context.Parametro.Where(p => p.id_padre == 1).ToList();

            return parametro;
        }

        [HttpGet("api/PreguntasEntrevistaCompetencias")]
        public IEnumerable<Parametro> GetPreguntasEntrevistaCompetencias()
        {

            var parametro = context.Parametro.Where(p => p.id_padre == 2).ToList();

            return parametro;
        }

        [HttpGet("api/PreguntasEntrevistaTecnica")]
        public IEnumerable<Parametro> GetPreguntasEntrevistaTecnica()
        {

            var parametro = context.Parametro.Where(p => p.id_padre == 3).ToList();

            return parametro;
        }

        [HttpGet("api/PreguntasEntrevistaCompetencias_Nivel2")]
        public IEnumerable<Parametro> GetPreguntasEntrevistaCompetencias_Nivel2()
        {

            var parametro = context.Parametro.Where(p => p.id_padre == 13).ToList();

            return parametro;
        }

        [HttpGet("api/PreguntasEntrevistaTecnica_Nivel2")]
        public IEnumerable<Parametro> GetPreguntasEntrevistaTecnica_Nivel2()
        {

            var parametro = context.Parametro.Where(p => p.id_padre == 19).ToList();

            return parametro;
        }

        // GET api/<SeleccionController>/5
        [HttpGet("{id}")]
        public Seleccion Get(int id)
        {
            var postulante = context.Seleccion_cabecera.FirstOrDefault(p => p.seleccion_id == id);
            return postulante;
        }

        // POST api/<SeleccionController>
        [HttpPost]
        public ActionResult GrabarSeleccionCabecera([FromBody] Seleccion seleccion)
        {
            try
            {
                context.Seleccion_cabecera.Add(seleccion);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        // PUT api/<SeleccionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SeleccionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
