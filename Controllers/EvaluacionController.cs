using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Ficha;
using SIGED_API.Models;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;
using SIGED_API.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/evaluacion")]
    [ApiController]
   // [Authorize]
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
        //[HttpGet("{especialidad_id}")]
        //public IEnumerable<DetalleEvaluacion> Get(int especialidad_id)
        //{
        //    List<DetalleEvaluacion> evaluacion = new List<DetalleEvaluacion>();


        //   var evaluacionpost = context2.EVALUACION.Join(context2.DETALLE_EVALUACION,
        //       sd => sd.evaluacion_id,
        //       r => r.EVALUACION_ID,
        //       (sd, r) => new { sd, r }
        //       ).Where(c => c.sd.especialidad_id == especialidad_id).FirstOrDefault();


        //    if (evaluacionpost != null)
        //    {

        //        evaluacion = context2.Postulante.
        //        Join(context2.DETALLE_EVALUACION,
        //        p => p.postulante_id,
        //        ai => ai.POSTULANTE_ID,
        //        (p, ai) => new { p, ai }
        //        )
        //        .Join(context2.EVALUACION,
        //        a => a.ai.EVALUACION_ID,
        //        ai => ai.evaluacion_id,
        //        (a, ai) => new { a, ai }
        //        ).Where(c => c.ai.especialidad_id == especialidad_id
        //        ).Select(res => new DetalleEvaluacion()
        //        {
        //            postulante_id = res.a.ai.POSTULANTE_ID,
        //            nombre_completo = res.a.p.ape_paterno + " " + res.a.p.ape_materno + " " + res.a.p.nombre,
        //            evaluacion_id = res.ai.evaluacion_id,
        //            detalle_evaluacion_id = res.a.ai.DETALLE_EVALUACION_ID,
        //            enc_estu = res.a.ai.ENC_ESTU,
        //            cap_doc = res.a.ai.CAP_DOC,
        //            acom_doc = res.a.ai.ACOM_DOC,
        //            cum_adm = res.a.ai.CUM_ADM,
        //            cum_vir = res.a.ai.CUM_VIR,
        //            nota_final = res.a.ai.NOTA_FINAL


        //        }).ToList();

        //        return evaluacion;
        //    }
        //    else
        //    {
        //        evaluacion = context2.Especialidad_postulante.Join(context2.Postulante,
        //        sd => sd.postulante_id,
        //        r => r.postulante_id,
        //        (sd, r) => new { sd, r }
        //        ).Select(res => new DetalleEvaluacion()
        //        {
        //            postulante_id = res.r.postulante_id,
        //            nombre_completo = res.r.ape_paterno + " " + res.r.ape_materno + " " + res.r.nombre,


        //        }).ToList();

        //        return evaluacion; }
            
        //}

        // GET api/<EvaluacionController>/5
     /*   [HttpGet("Iniciar/{especialidad_id}")]
        public IEnumerable<DetalleEvaluacion> GetIniciar(int especialidad_id)
        {
            List<DetalleEvaluacion> evaluacion = new List<DetalleEvaluacion>();


            var evaluacionpost = context2.EVALUACION.Join(context2.DETALLE_EVALUACION,
                sd => sd.evaluacion_id,
                r => r.EVALUACION_ID,
                (sd, r) => new { sd, r }
                ).Where(c => c.sd.especialidad_id == especialidad_id).FirstOrDefault();


            if (evaluacionpost != null)
            {


                evaluacion = context2.EVALUACION.
                Join(context2.DETALLE_EVALUACION,
                p => p.evaluacion_id,
                ai => ai.EVALUACION_ID,
                (p, ai) => new { p, ai }
                )
                .Join(context2.Postulante,
                a => a.ai.POSTULANTE_ID,
                ai => ai.postulante_id,
                (a, ai) => new { a, ai }
                ).Where(c => c.a.p.especialidad_id == especialidad_id
                ).Select(res => new DetalleEvaluacion()
                {
                    postulante_id = res.a.ai.POSTULANTE_ID,
                    nombre_completo = res.ai.ape_paterno + " " + res.ai.ape_materno + " " + res.ai.nombre,
                    evaluacion_id = res.a.ai.EVALUACION_ID,
                    detalle_evaluacion_id = res.a.ai.DETALLE_EVALUACION_ID,
                    enc_estu = res.a.ai.ENC_ESTU,
                    cap_doc = res.a.ai.CAP_DOC,
                    acom_doc = res.a.ai.ACOM_DOC,
                    cum_adm = res.a.ai.CUM_ADM,
                    cum_vir = res.a.ai.CUM_VIR,
                    nota_final = res.a.ai.NOTA_FINAL


                }).ToList();

                return evaluacion;
            }
            else
            {
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


           
            
          
        }*/

        // POST api/<EvaluacionController>
        [HttpPost("Grabar")]
        public Respuesta Post([FromBody] EvaluacionDetalleRequest evaluacionDetalleRequest)
        {
            var respuesta = new Respuesta();
            respuesta.status = true;

            if (evaluacionDetalleRequest != null && evaluacionDetalleRequest.flagTipo == "G")
            {
                foreach (detalleEvaluacion detalleEvaluacion in evaluacionDetalleRequest.detalle_evaluacion)
                {
                    var evaluacionPostulante = context2.DETALLE_EVALUACION.FirstOrDefault(d => d.POSTULANTE_ID == detalleEvaluacion.postulante_id && d.SEMESTRE_ID == evaluacionDetalleRequest.semestre_id);
                    if (evaluacionPostulante != null)
                    {
                        if (evaluacionPostulante.ESTADO.Equals('G'))
                        {
                            evaluacionPostulante.ENC_ESTU = detalleEvaluacion.enc_estu;
                            evaluacionPostulante.CUM_ADM = detalleEvaluacion.cum_adm;
                            evaluacionPostulante.CAP_DOC = detalleEvaluacion.cap_doc;
                            evaluacionPostulante.ACOM_DOC = detalleEvaluacion.acom_doc;
                            evaluacionPostulante.CUM_VIR = detalleEvaluacion.cum_vir;
                            evaluacionPostulante.NOTA_FINAL = detalleEvaluacion.nota_final;
                            evaluacionPostulante.FECHA_GUARDADO = evaluacionDetalleRequest.fecha;
                            evaluacionPostulante.EVALUADOR_ID = evaluacionDetalleRequest.evaluador_id;
                            context2.Entry(evaluacionPostulante).State = EntityState.Modified;
                            context2.SaveChanges();
                        }
                    }
                    else
                    {
                        var evaluaciondetalleTemp = new DETALLE_EVALUACION();
                        evaluaciondetalleTemp.EVALUADOR_ID = evaluacionDetalleRequest.evaluador_id;
                        evaluaciondetalleTemp.POSTULANTE_ID = detalleEvaluacion.postulante_id;
                        evaluaciondetalleTemp.ENC_ESTU = detalleEvaluacion.enc_estu;
                        evaluaciondetalleTemp.CUM_ADM = detalleEvaluacion.cum_adm;
                        evaluaciondetalleTemp.CAP_DOC = detalleEvaluacion.cap_doc;
                        evaluaciondetalleTemp.ACOM_DOC = detalleEvaluacion.acom_doc;
                        evaluaciondetalleTemp.CUM_VIR = detalleEvaluacion.cum_vir;
                        evaluaciondetalleTemp.NOTA_FINAL = detalleEvaluacion.nota_final;
                        evaluaciondetalleTemp.FECHA_GUARDADO = evaluacionDetalleRequest.fecha;
                        evaluaciondetalleTemp.ESTADO = 'G';
                        evaluaciondetalleTemp.SEMESTRE_ID = evaluacionDetalleRequest.semestre_id;
                        context2.DETALLE_EVALUACION.Add(evaluaciondetalleTemp);
                        context2.SaveChanges();
                    }
                }
            }
            else
            {
                foreach (detalleEvaluacion detalleEvaluacion in evaluacionDetalleRequest.detalle_evaluacion)
                {
                    var evaluacionPostulante = context2.DETALLE_EVALUACION.FirstOrDefault(d => d.POSTULANTE_ID == detalleEvaluacion.postulante_id && d.SEMESTRE_ID == evaluacionDetalleRequest.semestre_id);
                    if (evaluacionPostulante != null)
                    {
                        if (evaluacionPostulante.ESTADO.Equals('G'))
                        {
                            evaluacionPostulante.ENC_ESTU = detalleEvaluacion.enc_estu;
                            evaluacionPostulante.CUM_ADM = detalleEvaluacion.cum_adm;
                            evaluacionPostulante.CAP_DOC = detalleEvaluacion.cap_doc;
                            evaluacionPostulante.ACOM_DOC = detalleEvaluacion.acom_doc;
                            evaluacionPostulante.CUM_VIR = detalleEvaluacion.cum_vir;
                            evaluacionPostulante.NOTA_FINAL = detalleEvaluacion.nota_final;
                            evaluacionPostulante.FECHA_REGISTRO = evaluacionDetalleRequest.fecha;
                            evaluacionPostulante.EVALUADOR_ID = evaluacionDetalleRequest.evaluador_id;
                            evaluacionPostulante.ESTADO = 'R';
                            context2.Entry(evaluacionPostulante).State = EntityState.Modified;
                            context2.SaveChanges();
                        }
                    }
                }
            }

            return respuesta;
        }

        [HttpPut("statusEvaluacion")]
        public ActionResult ActualizarEstado([FromBody] EVALUACION evaluacion)
        {
            context2.Entry(evaluacion).State = EntityState.Modified;
            context2.SaveChanges();

            var result = new OkObjectResult(new { message = "OK", status = true, status_eva = evaluacion.estado, evaluacion_id = evaluacion.evaluacion_id });
            return result;
        }


     /*   [HttpPut("ActualizarDetalle")]
        public ActionResult ActualizarDetalle([FromBody] EvaluacionRequest evaluacion)
        {
            foreach (var odetalles in evaluacion.Detalle_evaluacion)
            {
                DETALLE_EVALUACION odetalle = new DETALLE_EVALUACION();
                odetalle.DETALLE_EVALUACION_ID = odetalles.detalle_evaluacion_id;
                odetalle.EVALUACION_ID = odetalles.evaluacion_id;
                odetalle.POSTULANTE_ID = odetalles.postulante_id;
                odetalle.ENC_ESTU = odetalles.enc_estu;
                odetalle.CUM_ADM = odetalles.cum_adm;
                odetalle.CAP_DOC = odetalles.cap_doc;
                odetalle.ACOM_DOC = odetalles.acom_doc;
                odetalle.CUM_VIR = odetalles.cum_adm;
                odetalle.NOTA_FINAL = odetalles.nota_final;
                context2.Entry(odetalle).State = EntityState.Modified;

                context2.SaveChanges();
            }
            return  new OkObjectResult(new { message = "OK", status = true,  evaluacion_id = evaluacion.evaluacion_id });
        }*/

        [HttpGet("EstadoEvaluacion/{especialidad_id}")]
        public EVALUACION EstadoEvaluacion(int especialidad_id)
        {
            return context2.EVALUACION.FirstOrDefault(p => p.especialidad_id == especialidad_id);
        }
}
}
