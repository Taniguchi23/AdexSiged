using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Ficha;
using SIGED_API.Models;
using SIGED_API.Models.Request;
using SIGED_API.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/evaluacion")]
    [ApiController]
    [Authorize]
    public class EvaluacionController : ControllerBase
    {

        private readonly AppDbContext2 context2;
        private readonly IWebHostEnvironment webHostEnviroment;

        public EvaluacionController( AppDbContext2 context2, IWebHostEnvironment webHost)
        {
            this.context2 = context2;
            webHostEnviroment = webHost;
        }

        // GET: api/<EvaluacionController>
        [HttpGet]
        public IEnumerable<DetalleEvaluacion> Get()
        {
            List<DetalleEvaluacion> evaluacion = new List<DetalleEvaluacion>();



            evaluacion = context2.Especialidad_postulante.Join(context2.Postulante,
            sd => sd.postulante_id,
            r => r.postulante_id,
            (sd, r) => new { sd, r }
            ).Select(res => new DetalleEvaluacion()
            {
                postulante_id = res.r.postulante_id,
                nombre_completo = res.r.ape_paterno + " " + res.r.ape_materno + " " + res.r.nombre,


            }).ToList();


            return evaluacion;
        }

        // GET api/<EvaluacionController>/5
        [HttpGet("Iniciar/{especialidad_id}")]
        public IEnumerable<DetalleEvaluacion> Get(int especialidad_id)
        {
            List<DetalleEvaluacion> evaluacion = new List<DetalleEvaluacion>();


           
               evaluacion = context2.Especialidad_postulante.Join(context2.Postulante,
               sd => sd.postulante_id,
               r => r.postulante_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.sd.especialidad_id == especialidad_id)
               .Select(res => new DetalleEvaluacion()
               {
                   postulante_id = res.r.postulante_id,
                   nombre_completo = res.r.ape_paterno + " " + res.r.ape_materno + " " + res.r.nombre,


               }).ToList();

       
            return evaluacion;
            
          
        }

        // POST api/<EvaluacionController>
        [HttpPost("Grabar")]
        public ActionResult Post([FromBody] EvaluacionRequest evaluacion)
        {
            var result = new OkObjectResult(0);

            try
            {
                

                var vevaluacion = context2.EVALUACION.FirstOrDefault(p => p.POSTULANTE_ID == evaluacion.postulante_id & p.COORDINADOR_ID == evaluacion.coordinador_id);

                if (vevaluacion != null)
                {


                    result = new OkObjectResult(new { message = "Ya existe", status = true, status_reg = vevaluacion.ESTADO,  EVALUACION = vevaluacion.EVALUACION_ID });


                    foreach (var odetalles in evaluacion.Detalle_evaluacion)
                    {
                        DETALLE_EVALUACION odetalle = new DETALLE_EVALUACION();
                        odetalle.DETALLE_EVALUACION_ID = odetalles.detalle_evaluacion_id;
                        odetalle.EVALUACION_ID = evaluacion.evaluacion_id;
                        odetalle.POSTULANTE_ID = evaluacion.postulante_id;
                        odetalle.ENC_ESTU = odetalles.enc_estu;
                        odetalle.CUM_ADM = odetalles.cum_adm;
                        odetalle.CAP_DOC = odetalles.cap_doc;
                        odetalle.ACOM_DOC = odetalles.acom_doc;
                        odetalle.CUM_VIR = odetalles.cum_adm;
                        odetalle.NOTA_FINAL = odetalles.nota_final;
                        context2.Entry(odetalle).State = EntityState.Modified;
                   
                        context2.SaveChanges();
                    }
                    result = new OkObjectResult(new { message = "OK", status = true });

                }

                else
                {
                    EVALUACION oevaluacion = new EVALUACION();
                    oevaluacion.POSTULANTE_ID = evaluacion.postulante_id;
                    oevaluacion.COORDINADOR_ID = evaluacion.coordinador_id;
                    oevaluacion.ESTADO = false;
                    oevaluacion.FECHA = evaluacion.fecha;
                    context2.EVALUACION.Add(oevaluacion);
                    context2.SaveChanges();

                   
                    if (evaluacion.Detalle_evaluacion != null) 
                    
                    { 

                    foreach (var odetalles in evaluacion.Detalle_evaluacion)
                    {
                        DETALLE_EVALUACION odetalle = new DETALLE_EVALUACION();
                        odetalle.EVALUACION_ID = oevaluacion.EVALUACION_ID;
                        odetalle.POSTULANTE_ID = evaluacion.postulante_id;
                        odetalle.ENC_ESTU = odetalles.enc_estu;
                        odetalle.CUM_ADM = odetalles.cum_adm;
                        odetalle.CAP_DOC = odetalles.cap_doc;
                        odetalle.ACOM_DOC = odetalles.acom_doc;
                        odetalle.CUM_VIR = odetalles.cum_adm;
                        odetalle.NOTA_FINAL = odetalles.nota_final;

                        context2.DETALLE_EVALUACION.Add(odetalle);
                        context2.SaveChanges();
                    }
                        result = new OkObjectResult(new { message = "OK", status = true });

                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                return BadRequest();
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
