using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/competenciaja")]
    [ApiController]
    [Authorize]
    public class CompetenciaJAController : ControllerBase
    {
        private readonly AppDbContext context;
        public CompetenciaJAController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<CompetenciaJAController>
        [HttpGet]
        public IEnumerable<E_JefeAcademico> Get()
        {
            return context.E_JefeAcademico.ToList();
        }

        // GET api/<CompetenciaJAController>/5
        [HttpGet("{id}")]
        public E_JefeAcademico Get(int id)
        {
            var jefeacademico = context.E_JefeAcademico.FirstOrDefault(p => p.entrevistaja_id == id);
            return jefeacademico;
        }

        // POST api/<CompetenciaJAController>
        [HttpPost]
        public ActionResult Post([FromBody] EvaluacionJA_Request juradoacademico)
        {
            try
            {
                E_JefeAcademico ojefeacedemico= new E_JefeAcademico();
                ojefeacedemico.fecha = juradoacademico.fecha;
                ojefeacedemico.apreciacion = juradoacademico.apreciacion;
                ojefeacedemico.id_hora_pedagogica = juradoacademico.id_hora_pedagogica;
                ojefeacedemico.id_cargo = juradoacademico.id_cargo;
                ojefeacedemico.observacion = juradoacademico.observacion;
                ojefeacedemico.estado = juradoacademico.estado;
                context.E_JefeAcademico.Add(ojefeacedemico);
                context.SaveChanges();

                Seleccion_detalle oseleccion_detalle = new Seleccion_detalle();
                oseleccion_detalle.seleccion_id = juradoacademico.seleccion_id;
                oseleccion_detalle.entrevistaja_id = ojefeacedemico.entrevistaja_id;
                context.Seleccion_detalle.Add(oseleccion_detalle);
                context.SaveChanges();
                var result = new OkObjectResult(new { message = "OK", status = true });
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // PUT api/<CompetenciaJAController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CompetenciaJAController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
