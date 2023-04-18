using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models.Dao;
using SIGED_API.Models.Response;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/rubrica")]
    [ApiController]
   // [Authorize]
    public class RubricaController : ControllerBase
    {
        private readonly AppDbContext context;

        public RubricaController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<RubricaController>
        [HttpGet]
        public Respuesta Get()
        {
            var respuesta = new Respuesta();
            respuesta.status = true;
            var rubricas = context.Rubrica.ToList();
            var listaRubricas = new List<RubricaDao>();
            foreach(Rubrica rubrica in rubricas)
            {
                var rubricaDao = new RubricaDao();
                var detalles = context.Rubrica_Detalle.Where(r => r.Rubrica_id == rubrica.rubrica_id && r.Estado == 'A').OrderBy(p=> p.Puntaje).ToList();
                if(detalles.Count > 0)
                {
                    var listaPuntaje = detalles.Select(p=>p.Puntaje).ToList();
                    rubricaDao.ListaPuntajes = listaPuntaje;
                }
                rubricaDao.Rubrica = rubrica;
                rubricaDao.ListaRubrica = detalles;
                listaRubricas.Add(rubricaDao);
            }

            respuesta.Data = listaRubricas;

            return respuesta;

        }

        // GET api/<RubricaController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<RubricaController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RubricaController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RubricaController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
