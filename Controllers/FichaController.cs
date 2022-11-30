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
        public ActionResult Post([FromBody] FichaDatosRequest ficha)
        {
            try
            {

                Estudio_Realizado oestudio = new Estudio_Realizado();
                oestudio.postulante_id = ficha.postulante_id;
                oestudio.otros = ficha.otros;
                oestudio.otros = ficha.otros_programas;
                context2.Estudio_Realizado.Add(oestudio);
                context2.SaveChanges();


                foreach (var opregrados in ficha.Pregrados)
                {
                    Pregrado opregrado = new Pregrado();
                    opregrado.estudio_id = oestudio.estudio_id;
                    opregrado.centro_estudio = opregrados.centro_estudio;
                    opregrado.carrera = opregrados.carrera;
                    opregrado.grado_acad = opregrados.grado_acad;
                    opregrado.fecha_ingreso = opregrados.fecha_ingreso;
                    opregrado.fecha_salida = opregrados.fecha_salida;
                    context2.Pregrado.Add(opregrado);
                    context2.SaveChanges();
                }


                foreach (var opostgrados in ficha.Postgrados)
                {
                    Postgrado opostgrado = new Postgrado();
                    opostgrado.estudio_id = oestudio.estudio_id;
                    opostgrado.centro_estudio = opostgrados.centro_estudio;
                    opostgrado.especializacion = opostgrados.especializacion;
                    opostgrado.nivel = opostgrados.nivel;
                    opostgrado.fecha_ingreso = opostgrados.fecha_ingreso;
                    opostgrado.fecha_salida = opostgrados.fecha_salida;
                    context2.Postgrado.Add(opostgrado);
                    context2.SaveChanges();
                }




                foreach (var oPostingles in ficha.Idioma_Ingles)
                {
                    NIVEL_INGLES oingles = new NIVEL_INGLES();
                    oingles.ESTUDIO_ID = oestudio.estudio_id;
                    oingles.IDIOMA_ID = oPostingles.idioma_id;
                    oingles.NIVELESTUDIO_ID = oPostingles.nivel_ingles_id;
                    context2.NIVEL_INGLES.Add(oingles);
                    context2.SaveChanges();
                }




         
                foreach (var oPostofimatica in ficha.Ofimatica)
                {
                    NIVEL_OFIMATICA oofimatica = new NIVEL_OFIMATICA();
                    oofimatica.ESTUDIO_ID = oestudio.estudio_id;
                    oofimatica.OFIMATICA_ID = oPostofimatica.ofimatica_id;
                    oofimatica.NIVELESTUDIO_ID = oPostofimatica.nivel_ofimatica_id;
                    context2.NIVEL_OFIMATICA.Add(oofimatica);
                    context2.SaveChanges();
                }


                EXPERIENCIA oexperiencia = new EXPERIENCIA();
                oexperiencia.POSTULANTE_ID = ficha.postulante_id;
                context2.EXPERIENCIA.Add(oexperiencia);
                context2.SaveChanges();
                 

           
                foreach (var oPostexperiencialaboral in ficha.Experiencia)
                {
                    EXPERIENCIA_LABORAL oexperiencialaboral = new EXPERIENCIA_LABORAL();
                    oexperiencialaboral.EXPERIENCIA_ID = oexperiencia.EXPERIENCIA_ID;
                    oexperiencialaboral.EMPRESA = oPostexperiencialaboral.empresa;
                    oexperiencialaboral.CARGO = oPostexperiencialaboral.cargo;
                    oexperiencialaboral.JEFE_INMEDIATO = oPostexperiencialaboral.jefe_inmediato;
                    oexperiencialaboral.TELEFONO = oPostexperiencialaboral.telefono;
                    oexperiencialaboral.FECHA_INGRESO = oPostexperiencialaboral.fecha_ingreso;
                    oexperiencialaboral.FECHA_CESE = oPostexperiencialaboral.fecha_cese;
                    oexperiencialaboral.MOTIVO_CESE = oPostexperiencialaboral.motivo_cese;
                    context2.EXPERIENCIA_LABORAL.Add(oexperiencialaboral);
                    context2.SaveChanges();
                }


                COMPOSICION_FAMILIAR ocomposicionfamiliar= new COMPOSICION_FAMILIAR();
                ocomposicionfamiliar.POSTULANTE_ID = ficha.postulante_id;
                ocomposicionfamiliar.NOMBRE = ficha.nombre;
                ocomposicionfamiliar.APELLIDO_PATERNO = ficha.apellido_paterno;
                ocomposicionfamiliar.APELLIDO_MATERNO = ficha.apellido_materno;
                ocomposicionfamiliar.DNI = ficha.dni;
                ocomposicionfamiliar.FECHA = ficha.fecha;
                ocomposicionfamiliar.EDAD = ficha.edad;
                context2.COMPOSICION_FAMILIAR.Add(ocomposicionfamiliar);
                context2.SaveChanges();

             
                foreach (var oPostcomposicionhijo in ficha.Hijos)
                {
                    COMPOSICION_HIJO ocomposicionhijo = new COMPOSICION_HIJO();
                    ocomposicionhijo.COMPOSICION_ID = ocomposicionfamiliar.COMPOSICION_ID;
                    ocomposicionhijo.NOMBRE = oPostcomposicionhijo.nombre;
                    ocomposicionhijo.APELLIDO_PATERNO = oPostcomposicionhijo.apellido_paterno;
                    ocomposicionhijo.APELLIDO_MATERNO = oPostcomposicionhijo.apellido_materno;
                    ocomposicionhijo.DNI = oPostcomposicionhijo.dni;
                    ocomposicionhijo.FECHA = oPostcomposicionhijo.fecha;
                    ocomposicionhijo.EDAD = oPostcomposicionhijo.edad;
                    context2.COMPOSICION_HIJO.Add(ocomposicionhijo);
                    context2.SaveChanges();
                }

                PAGO opago = new PAGO();
                opago.POSTULANTE_ID = ficha.postulante_id;
                opago.NRO_CUENTA = ficha.nro_cuenta;
                opago.BANCO_ID = ficha.banco_id;
                opago.CCI = ficha.cci;
                opago.SISTEMA_PEN = ficha.sistema_pen;
                opago.AFP_ID = ficha.afp_id;
                opago.OTROS_BANCOS = ficha.otros_bancos;
                context2.PAGO.Add(opago);
                context2.SaveChanges();

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
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
