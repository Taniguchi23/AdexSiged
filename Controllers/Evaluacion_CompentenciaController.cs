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
    public class Evaluacion_CompentenciaController : ControllerBase
    {
        private readonly AppDbContext context;

        public Evaluacion_CompentenciaController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<Evaluacion_CompentenciaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Evaluacion_CompentenciaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Evaluacion_CompentenciaController>
        [HttpPost]
        public ActionResult GrabarRevisionCV([FromBody] RequestCompetencia competencia)
        {
            try
            {
                E_Competencia ocompetencia = new E_Competencia();
                ocompetencia.fecha = competencia.fecha;
                ocompetencia.comentario_1 = competencia.comentario_1;
                ocompetencia.comentario_2 = competencia.comentario_2;
                ocompetencia.comentario_3 = competencia.comentario_3;
                ocompetencia.comentario_4 = competencia.comentario_4;
                ocompetencia.comentario_5 = competencia.comentario_5;
                ocompetencia.comentario_6 = competencia.comentario_6;
                ocompetencia.comentario_7 = competencia.comentario_7;
                ocompetencia.comentario_8 = competencia.comentario_8;
                ocompetencia.comentario_9 = competencia.comentario_9;
                ocompetencia.comentario_10 = competencia.comentario_10;
                ocompetencia.comentario_11 = competencia.comentario_11;
                ocompetencia.comentario_12 = competencia.comentario_12;
                ocompetencia.comentario_13 = competencia.comentario_13;
                ocompetencia.comentario_14 = competencia.comentario_14;
                ocompetencia.comentario_15 = competencia.comentario_15;
                ocompetencia.comentario_16 = competencia.comentario_16;
                ocompetencia.comentario_17 = competencia.comentario_17;
                ocompetencia.comentario_18 = competencia.comentario_18;
                ocompetencia.comentario_19 = competencia.comentario_19;
                ocompetencia.comentario_20 = competencia.comentario_20;
                ocompetencia.estado = competencia.estado;
                context.E_Competencia.Add(ocompetencia);
                context.SaveChanges();

                Seleccion_detalle oseleccion_detalle = new Seleccion_detalle();
                oseleccion_detalle.seleccion_id = competencia.seleccion_id;
                oseleccion_detalle.e_competencia_id = ocompetencia.e_competencia_id;
                context.Seleccion_detalle.Add(oseleccion_detalle);
                context.SaveChanges();

                foreach (var oModelHab_Competencia in competencia.E_Habilidad_Competencias)
                {
                    E_Habilidad_Competencia ohabilidad_competencia = new E_Habilidad_Competencia();
                    ohabilidad_competencia.e_competencia_i = ocompetencia.e_competencia_id;
                    ohabilidad_competencia.id_habilidad = oModelHab_Competencia.id_habilidad;
                    ohabilidad_competencia.id_valoracion = oModelHab_Competencia.id_valoracion;
                    context.E_Habilidad_Competencia.Add(ohabilidad_competencia);
                    context.SaveChanges();
                }

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        // PUT api/<Evaluacion_CompentenciaController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Evaluacion_CompentenciaController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
