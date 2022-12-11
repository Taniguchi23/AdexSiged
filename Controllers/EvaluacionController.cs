using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionController : ControllerBase
    {

        private readonly AppDbContext2 context2;
        private readonly IWebHostEnvironment webHostEnviroment;

        public EvaluacionController(AppDbContext2 context2, IWebHostEnvironment webHost)
        {
            this.context2 = context2;
            webHostEnviroment = webHost;
        }

        // GET: api/<EvaluacionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EvaluacionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EvaluacionController>
        [HttpPost]
        public ActionResult Post([FromBody] EvaluacionRequest evaluacion)
        {
            try
            {

                EVALUACION oevaluacion = new EVALUACION();
                oevaluacion.POSTULANTE_ID = evaluacion.postulante_id;
                oevaluacion.COORDINADOR_ID = evaluacion.coordinador_id;
                oevaluacion.MODULO_ID = evaluacion.modulo_id;
                oevaluacion.FECHA = evaluacion.fecha;
                context2.EVALUACION.Add(oevaluacion);
                context2.SaveChanges();


                foreach (var odetalles in evaluacion.Detalle_evaluacion)
                {
                    DETALLE_EVALUACION odetalle = new DETALLE_EVALUACION();
                    odetalle.EVALUACION_ID = oevaluacion.EVALUACION_ID;
                    odetalle.COMPONENTE_ID = odetalles.componente_id;
                    odetalle.CALIFICACION = odetalles.calificacion;
                    odetalle.PUNTAJE = odetalles.puntaje;

                    context2.DETALLE_EVALUACION.Add(odetalle);
                    context2.SaveChanges();
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // PUT api/<EvaluacionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EvaluacionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
