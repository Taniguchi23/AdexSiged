using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using static SIGED_API.Models.RequestCompetencia;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/seleccion")]
    [ApiController]
    [Authorize]
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

            var parametroprincipal = context.Parametro.ToList();

            var parametro = context.Parametro.Where(p => p.id_padre == 2).ToList();

            List<int> parametro2 = new List<int>();
            foreach (var i in parametro)
            {
                parametro2.Add(i.parametro_id);
            }


            var result = from x in parametroprincipal
                         where parametro2.Contains(x.id_padre)
                         
                         select x;

            return result;

        }



        [HttpGet("api/PreguntasEntrevistaTecnica_Nivel2")]
        public IEnumerable<Parametro> GetPreguntasEntrevistaTecnica_Nivel2()
        {

            var parametroprincipal = context.Parametro.ToList();

            var parametro = context.Parametro.Where(p => p.id_padre == 3).ToList();

            List<int> parametro2 = new List<int>();
            foreach (var i in parametro)
            {
                parametro2.Add(i.parametro_id);
            }


            var result = from x in parametroprincipal
                         where parametro2.Contains(x.id_padre)

                         select x;

            return result;

        }
        //{correo}/{contrasena}
        // GET api/<SeleccionController>/5
        [HttpGet("{id}")]
        public Seleccion Get(int id)
        {
            var postulante = context.Seleccion_cabecera.FirstOrDefault(p => p.seleccion_id == id);
            return postulante;
        }

        [HttpGet("detalleseleccion/{id}")]
        public SeleccionInformacion GetSeleccionbyCode(int id)
        {
            SeleccionInformacion Objseleccion = new SeleccionInformacion();

            Revision _revision = new Revision();

             _revision = context.Seleccion_detalle.Join(context.RevisionCV,
                sd => sd.revision_id,
                r => r.revision_id,
                (sd,r) => new { sd, r }
                ).Where(c => c.sd.seleccion_id == id)
                .Select(res => new Revision()
                 {
                    revision_id = res.r.revision_id,
                    fecha_rev = res.r.fecha_rev,
                    seleccion_c1 = res.r.seleccion_c1,
                    seleccion_c2 = res.r.seleccion_c2,
                    seleccion_c3 = res.r.seleccion_c3,
                    seleccion_c4 = res.r.seleccion_c4,
                    seleccion_c5 = res.r.seleccion_c5,
                    seleccion_c6 = res.r.seleccion_c6,
                    comentario_1 = res.r.comentario_1,
                    comentario_2 = res.r.comentario_2,
                    comentario_3 = res.r.comentario_3,
                    comentario_4 = res.r.comentario_4,
                    comentario_5 = res.r.comentario_5,
                    comentario_6 = res.r.comentario_6,
                    observacion = res.r.observacion,
                    estado = res.r.estado

                }).FirstOrDefault();

            Objseleccion.RevisionCV = _revision;

            E_Competencia _competencia = new E_Competencia();

            _competencia = context.Seleccion_detalle.Join(context.E_Competencia,
               sd => sd.e_competencia_id,
               r => r.e_competencia_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.sd.seleccion_id == id)
               .Select(res => new E_Competencia()
               {
                   e_competencia_id = res.r.e_competencia_id,
                   fecha = res.r.fecha,
                   comentario_1 = res.r.comentario_1,
                   comentario_2 = res.r.comentario_2,
                   comentario_3 = res.r.comentario_3,
                   comentario_4 = res.r.comentario_4,
                   comentario_5 = res.r.comentario_5,
                   comentario_6 = res.r.comentario_6,
                   comentario_7 = res.r.comentario_7,
                   comentario_8 = res.r.comentario_8,
                   comentario_9 = res.r.comentario_9,
                   comentario_10 = res.r.comentario_10,
                   comentario_11 = res.r.comentario_11,
                   comentario_12 = res.r.comentario_12,
                   comentario_13 = res.r.comentario_13,
                   comentario_14 = res.r.comentario_14,
                   comentario_15 = res.r.comentario_15,
                   comentario_16 = res.r.comentario_16,
                   comentario_17 = res.r.comentario_17,
                   comentario_18 = res.r.comentario_18,
                   comentario_19 = res.r.comentario_19,
                   comentario_20 = res.r.comentario_20

               }).FirstOrDefault();

            Objseleccion.E_Competencia = _competencia;

            //RequestCompetencia _competenciab = new RequestCompetencia();

            //_competenciab = context.Seleccion_detalle.Join(context.E_Competencia,
            //   sd => sd.e_competencia_id,
            //   r => r.e_competencia_id,
            //   (sd, r) => new { sd, r }
            //   ).Where(c => c.sd.seleccion_id == id)
            //   .Select(res => new RequestCompetencia()
            //   {
            //       fecha = res.r.fecha,
            //       comentario_1 = res.r.comentario_1
                  

            //   }).FirstOrDefault();

            //Objseleccion.E_Competenciab = _competenciab;


            E_Tecnica _tecnica = new E_Tecnica();

            _tecnica = context.Seleccion_detalle.Join(context.E_Tecnica,
               sd => sd.e_tecnica_id,
               r => r.e_tecnica_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.sd.seleccion_id == id)
               .Select(res => new E_Tecnica()
               {
                   e_tecnica_id = res.r.e_tecnica_id,
                   fecha = res.r.fecha,
                   comentario_1 = res.r.comentario_1,
                   comentario_2 = res.r.comentario_2,
                   comentario_3 = res.r.comentario_3,
                   comentario_4 = res.r.comentario_4,
                   comentario_5 = res.r.comentario_5,
                   comentario_6 = res.r.comentario_6,
                   comentario_7 = res.r.comentario_7,
                   comentario_8 = res.r.comentario_8,
                   comentario_9 = res.r.comentario_9,
                   comentario_10 = res.r.comentario_10,
                   comentario_11 = res.r.comentario_11,
                   apreciacion = res.r.apreciacion,
                   id_hora_pedagogica = res.r.id_hora_pedagogica,
                   observacion = res.r.observacion,
                   estado = res.r.estado

               }).FirstOrDefault();

            Objseleccion.E_Tecnica = _tecnica;


            E_JefeAcademico _jefeacademico = new E_JefeAcademico();

            _jefeacademico = context.Seleccion_detalle.Join(context.E_JefeAcademico,
               sd => sd.entrevistaja_id,
               r => r.entrevistaja_id,
               (sd, r) => new { sd, r }
               ).Where(c => c.sd.seleccion_id == id)
               .Select(res => new E_JefeAcademico()
               {
                   entrevistaja_id = res.r.entrevistaja_id,
                   fecha = res.r.fecha,
                   apreciacion = res.r.apreciacion,
                   id_hora_pedagogica = res.r.id_hora_pedagogica,
                   observacion = res.r.observacion,
                   id_cargo = res.r.id_cargo,
                   estado = res.r.estado

               }).FirstOrDefault();

            Objseleccion.E_JefeAcademico = _jefeacademico;

            return Objseleccion;

        }

        [HttpGet("traerseleccion/{idpostulante}/{idsemestre}/{idarea}")]
        public Seleccion GetSeleccionbyParameter(int idpostulante, int idsemestre, int idarea)
        {
            var postulante = context.Seleccion_cabecera.FirstOrDefault(p => p.postulante_id == idpostulante || p.semestre_id == idsemestre || p.area_id == idarea);
            return postulante;
        }


        // POST api/<SeleccionController>
        [HttpPost]
        public int GrabarSeleccionCabecera([FromBody] Seleccion seleccion)
        {
            try
            {
                var postulante = context.Seleccion_cabecera.FirstOrDefault(p => p.postulante_id == seleccion.postulante_id & p.semestre_id == seleccion.semestre_id & p.area_id == seleccion.area_id);

                if (postulante  != null)
                {
                    return postulante.seleccion_id;
                }
                else
                {
                    context.Seleccion_cabecera.Add(seleccion);
                    context.SaveChanges();
                    return seleccion.seleccion_id;
                }
               
                
            }
            catch (Exception ex)
            {
                return seleccion.seleccion_id;
                //return BadRequest();
            }

        }


        // PUT api/<SeleccionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SeleccionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
