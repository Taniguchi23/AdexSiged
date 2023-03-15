using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SIGED_API.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SIGED_API.Ficha;
using SIGED_API.Models;
using Postulante = SIGED_API.Ficha.Postulante;
using SIGED_API.Entity;
using System.Threading.Tasks;
using System.IO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/ficha")]
    [ApiController]
    //[Authorize]
    public class FichaController : ControllerBase
    {

        private readonly AppDbContext3 context3;
        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly ILogger<FichaController> logger;

        public FichaController(AppDbContext3 context3, IWebHostEnvironment webHost)
        {
            this.context3 = context3;
            webHostEnviroment = webHost;
        }
        // GET: api/<FichaController>
        [HttpGet]
        public IEnumerable<Postulante> GetFicha()
        {
            try
            {
                return context3.Postulante.ToList();
                //return Postulante();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error" + ex.Message);
                throw;
            }
        }

        // GET api/<FichaController>/5
        [HttpGet("{id}")]
        public Postulante Get(int id)
        {
          

           return  context3.Postulante.FirstOrDefault(p => p.postulante_id == id);
        }

        //POST api/<FichaController>
        [HttpPost]
        public ActionResult Post([FromBody] FichaDatosRequest ficha)
        {
            try
            {

                var temporal_imagen = context3.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 1 & p.modulo == 2);

                Estudio_Realizado oestudio = new Estudio_Realizado();
                oestudio.postulante_id = ficha.postulante_id;
                oestudio.otros = ficha.otros;
                oestudio.otros = ficha.otros_programas;
                context3.Estudio_Realizado.Add(oestudio);
                context3.SaveChanges();


                foreach (var opregrados in ficha.Pregrados)
                {
                    Pregrado opregrado = new Pregrado();
                    opregrado.estudio_id = oestudio.estudio_id;
                    opregrado.centro_estudio = opregrados.centro_estudio;
                    opregrado.carrera = opregrados.carrera;
                    opregrado.grado_acad = opregrados.grado_acad;
                    opregrado.fecha_ingreso = opregrados.fecha_ingreso;
                    opregrado.fecha_salida = opregrados.fecha_salida;
                    context3.Pregrado.Add(opregrado);
                    context3.SaveChanges();
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
                    context3.Postgrado.Add(opostgrado);
                    context3.SaveChanges();
                }




                foreach (var oPostingles in ficha.Idioma_Ingles)
                {
                    NIVEL_INGLES oingles = new NIVEL_INGLES();
                    oingles.ESTUDIO_ID = oestudio.estudio_id;
                    oingles.IDIOMA_ID = oPostingles.idioma_id;
                    oingles.NIVELESTUDIO_ID = oPostingles.nivel_ingles_id;
                    context3.NIVEL_INGLES.Add(oingles);
                    context3.SaveChanges();
                }





                foreach (var oPostofimatica in ficha.Ofimatica)
                {
                    NIVEL_OFIMATICA oofimatica = new NIVEL_OFIMATICA();
                    oofimatica.ESTUDIO_ID = oestudio.estudio_id;
                    oofimatica.OFIMATICA_ID = oPostofimatica.ofimatica_id;
                    oofimatica.NIVELESTUDIO_ID = oPostofimatica.nivel_ofimatica_id;
                    context3.NIVEL_OFIMATICA.Add(oofimatica);
                    context3.SaveChanges();
                }


                EXPERIENCIA oexperiencia = new EXPERIENCIA();
                oexperiencia.POSTULANTE_ID = ficha.postulante_id;
                context3.EXPERIENCIA.Add(oexperiencia);
                context3.SaveChanges();



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
                    context3.EXPERIENCIA_LABORAL.Add(oexperiencialaboral);
                    context3.SaveChanges();
                }


                COMPOSICION_FAMILIAR ocomposicionfamiliar = new COMPOSICION_FAMILIAR();
                ocomposicionfamiliar.POSTULANTE_ID = ficha.postulante_id;
                ocomposicionfamiliar.NOMBRE = ficha.nombre;
                ocomposicionfamiliar.APELLIDO_PATERNO = ficha.apellido_paterno;
                ocomposicionfamiliar.APELLIDO_MATERNO = ficha.apellido_materno;
                ocomposicionfamiliar.DNI = ficha.dni;
                ocomposicionfamiliar.FECHA = ficha.fecha;
                ocomposicionfamiliar.EDAD = ficha.edad;
                context3.COMPOSICION_FAMILIAR.Add(ocomposicionfamiliar);
                context3.SaveChanges();


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
                    context3.COMPOSICION_HIJO.Add(ocomposicionhijo);
                    context3.SaveChanges();
                }

                PAGO opago = new PAGO();
                opago.POSTULANTE_ID = ficha.postulante_id;
                opago.NRO_CUENTA = ficha.nro_cuenta;
                opago.BANCO_ID = ficha.banco_id;
                opago.CCI = ficha.cci;
                opago.SISTEMA_PEN = ficha.sistema_pen;
                opago.AFP_ID = ficha.afp_id;
                //opago.OTROS_BANCOS = ficha.otros_bancos;
                context3.PAGO.Add(opago);
                context3.SaveChanges();



                if (temporal_imagen != null)
                {

                    //ommodelo.referencia = temporal_imagen.archivo;
                    context3.TEMPORAL_IMAGEN.Remove(temporal_imagen);
                    context3.SaveChanges();

                }

                DECLARACION_JURADA odeclaracion = new DECLARACION_JURADA();
                odeclaracion.postulante_id = ficha.postulante_id;
                odeclaracion.fecha = ficha.fecha;
                odeclaracion.firma = temporal_imagen.archivo;
                context3.DECLARACION_JURADA.Add(odeclaracion);
                context3.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return Ok("Failed");

            }
        }

        [HttpGet("TraerImagen/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {

            var postulante = context3.DECLARACION_JURADA.FirstOrDefault(p => p.postulante_id == id);

            string path = webHostEnviroment.ContentRootPath + "\\images\\ficha\\";
            var filePath = path + postulante.firma;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            return null;
        }

        private string UploadedImagePostulante(TemporalRequest temporal)
        {
            string uniqueFileName = null;
            if (temporal.FrontArchivo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images\\ficha");
                uniqueFileName = temporal.FrontArchivo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    temporal.FrontArchivo.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }

        [HttpPost("AdjuntarImagen/{id}")]
        public ActionResult PostImagen([FromForm] TemporalRequest temporal, int id)
        {
            try
            {
                TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                string uniqueFileName = UploadedImagePostulante(temporal);
                opostulante.archivo = uniqueFileName;
                opostulante.descripcion = uniqueFileName;
                opostulante.tipoarchivo = 1;
                opostulante.modulo = 3;
                context3.TEMPORAL_IMAGEN.Add(opostulante);
                context3.SaveChanges();


                var postulante = context3.DECLARACION_JURADA.FirstOrDefault(p => p.postulante_id == id);
                if (postulante != null) 
                {
                    postulante.postulante_id = id;
                    postulante.firma = uniqueFileName;
                    context3.Entry(postulante).State = EntityState.Modified;
                    context3.SaveChanges();

                }
                else
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<FichaController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<FichaController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
