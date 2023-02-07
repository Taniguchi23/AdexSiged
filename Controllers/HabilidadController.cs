using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/habilidad")]
    [ApiController]
    [Authorize]
    public class HabilidadController : ControllerBase
    {
        private readonly AppDbContext context;

        public HabilidadController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<HabilidadController>
        [HttpGet]
        public IEnumerable<Habilidad> Get()
        {

            return context.Habilidad.ToList();

        }



        // GET api/<HabilidadController>/5
        [HttpGet("{id}")]
        public Habilidad Get(int id)
        {
            var area = context.Habilidad.FirstOrDefault(p => p.id_habilidad == id);
            return area;
        }

        [HttpGet("TraerHabilidadxCompetencia/{id}")]
        public IEnumerable<E_Habilidad_Competencia> GetEspecialidadbyPostulante(int id)
        {

            var postulante = context.E_Competencia.
                Join(context.E_Habilidad_Competencia,
                p => p.e_competencia_id,
                ai => ai.e_competencia_i,
                (p, ai) => new { p, ai }
                
                ).Where(c => c.ai.e_competencia_i == id)
                .Select(res => new E_Habilidad_Competencia()
                {
                    id_detalle_e_h_competencia = res.ai.id_detalle_e_h_competencia,
                    e_competencia_i = res.ai.e_competencia_i,
                    id_habilidad = res.ai.id_habilidad,
                    id_valoracion = res.ai.id_valoracion

                }).ToList();

            return postulante;

        }
        // POST api/<HabilidadController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<HabilidadController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<HabilidadController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
