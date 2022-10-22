using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class FichaController : ControllerBase
    {

        private readonly AppDbContext2 context2;
        private readonly IWebHostEnvironment webHostEnviroment;

        public FichaController(AppDbContext2 context2, IWebHostEnvironment webHost)
        {
            this.context2 = context2;
            webHostEnviroment = webHost;
        }
        // GET: api/<FichaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FichaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FichaController>
        [HttpPost]
        public ActionResult Post([FromForm] FichaDatosRequest ficha)
        {
            try
            {

                Estudio_Realizado oestudio = new Estudio_Realizado();
                oestudio.postulante_id = ficha.postulante_id;
                oestudio.otros = ficha.otros;
                oestudio.otros = ficha.otros_programas;
                context2.Estudio_Realizado.Add(oestudio);
                context2.SaveChanges();

                List<Pregrado> pregrados = JsonConvert.DeserializeObject<List<Pregrado>>(ficha.Pregrados);
                foreach (var oPostpregrados in pregrados)
                {
                    Pregrado opregrado = new Pregrado();
                    opregrado.estudio_id = oestudio.estudio_id;
                    opregrado.centro_estudio = oPostpregrados.centro_estudio;
                    opregrado.carrera = oPostpregrados.carrera;
                    opregrado.grado_acad = oPostpregrados.grado_acad;
                    opregrado.fecha_ingreso = oPostpregrados.fecha_ingreso;
                    opregrado.fecha_salida = oPostpregrados.fecha_salida;
                    context2.Pregrado.Add(opregrado);
                    context2.SaveChanges();
                }

                List<Postgrado> postgrado = JsonConvert.DeserializeObject<List<Postgrado>>(ficha.Postgrados);
                foreach (var oPostpostgrados in postgrado)
                {
                    Postgrado opostgrado = new Postgrado();
                    opostgrado.estudio_id = oestudio.estudio_id;
                    opostgrado.centro_estudio = oPostpostgrados.centro_estudio;
                    opostgrado.especializacion = oPostpostgrados.especializacion;
                    opostgrado.nivel = oPostpostgrados.nivel;
                    opostgrado.fecha_ingreso = oPostpostgrados.fecha_ingreso;
                    opostgrado.fecha_salida = oPostpostgrados.fecha_salida;
                    context2.Postgrado.Add(opostgrado);
                    context2.SaveChanges();
                }


                List<NIVEL_INGLES> ingles = JsonConvert.DeserializeObject<List<NIVEL_INGLES>>(ficha.Idioma_Ingles);
                foreach (var oPostingles in ingles)
                {
                    NIVEL_INGLES oingles = new NIVEL_INGLES();
                    oingles.ESTUDIO_ID = oestudio.estudio_id;
                    oingles.IDIOMA_ID = oPostingles.IDIOMA_ID;
                    oingles.NIVELESTUDIO_ID = oPostingles.NIVELESTUDIO_ID;
                    context2.NIVEL_INGLES.Add(oingles);
                    context2.SaveChanges();
                }

                List<NIVEL_OFIMATICA> ofimaticas = JsonConvert.DeserializeObject<List<NIVEL_OFIMATICA>>(ficha.Ofimatica);
                foreach (var oPostofimatica in ofimaticas)
                {
                    NIVEL_OFIMATICA oofimatica = new NIVEL_OFIMATICA();
                    oofimatica.ESTUDIO_ID = oestudio.estudio_id;
                    oofimatica.OFIMATICA_ID = oPostofimatica.OFIMATICA_ID;
                    oofimatica.NIVELESTUDIO_ID = oPostofimatica.NIVELESTUDIO_ID;
                    context2.NIVEL_OFIMATICA.Add(oofimatica);
                    context2.SaveChanges();
                }


                EXPERIENCIA oexperiencia = new EXPERIENCIA();
                oexperiencia.POSTULANTE_ID = ficha.postulante_id;
                context2.EXPERIENCIA.Add(oexperiencia);
                context2.SaveChanges();

                List<EXPERIENCIA_LABORAL> experiencialaboral= JsonConvert.DeserializeObject<List<EXPERIENCIA_LABORAL>>(ficha.Experiencia);
                foreach (var oPostexperiencialaboral in experiencialaboral)
                {
                    EXPERIENCIA_LABORAL oexperiencialaboral = new EXPERIENCIA_LABORAL();
                    oexperiencialaboral.EXPERIENCIA_ID = oexperiencia.EXPERIENCIA_ID;
                    oexperiencialaboral.EMPRESA = oPostexperiencialaboral.EMPRESA;
                    oexperiencialaboral.CARGO = oPostexperiencialaboral.CARGO;
                    oexperiencialaboral.JEFE_INMEDIATO = oPostexperiencialaboral.JEFE_INMEDIATO;
                    oexperiencialaboral.TELEFONO = oPostexperiencialaboral.TELEFONO;
                    oexperiencialaboral.FECHA_INGRESO = oPostexperiencialaboral.FECHA_INGRESO;
                    oexperiencialaboral.FECHA_CESE = oPostexperiencialaboral.FECHA_CESE;
                    oexperiencialaboral.MOTIVO_CESE = oPostexperiencialaboral.MOTIVO_CESE;
                    context2.EXPERIENCIA_LABORAL.Add(oexperiencialaboral);
                    context2.SaveChanges();
                }


                COMPOSICION_FAMILIAR ocomposicionfamiliar= new COMPOSICION_FAMILIAR();
                ocomposicionfamiliar.POSTULANTE_ID = ficha.postulante_id;
                ocomposicionfamiliar.NOMBRE = ficha.NOMBRE;
                ocomposicionfamiliar.APELLIDO_PATERNO = ficha.APELLIDO_PATERNO;
                ocomposicionfamiliar.APELLIDO_MATERNO = ficha.APELLIDO_MATERNO;
                ocomposicionfamiliar.DNI = ficha.DNI;
                ocomposicionfamiliar.FECHA = ficha.FECHA;
                ocomposicionfamiliar.EDAD = ficha.EDAD;
                context2.COMPOSICION_FAMILIAR.Add(ocomposicionfamiliar);
                context2.SaveChanges();

                List<COMPOSICION_HIJO> composicionhijo = JsonConvert.DeserializeObject<List<COMPOSICION_HIJO>>(ficha.Hijos);
                foreach (var oPostcomposicionhijo in composicionhijo)
                {
                    COMPOSICION_HIJO ocomposicionhijo = new COMPOSICION_HIJO();
                    ocomposicionhijo.COMPOSICION_ID = ocomposicionfamiliar.COMPOSICION_ID;
                    ocomposicionhijo.NOMBRE = oPostcomposicionhijo.NOMBRE;
                    ocomposicionhijo.APELLIDO_PATERNO = oPostcomposicionhijo.APELLIDO_PATERNO;
                    ocomposicionhijo.APELLIDO_MATERNO = oPostcomposicionhijo.APELLIDO_MATERNO;
                    ocomposicionhijo.DNI = oPostcomposicionhijo.DNI;
                    ocomposicionhijo.FECHA = oPostcomposicionhijo.FECHA;
                    ocomposicionhijo.EDAD = oPostcomposicionhijo.EDAD;
                    context2.COMPOSICION_HIJO.Add(ocomposicionhijo);
                    context2.SaveChanges();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // PUT api/<FichaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FichaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
