using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Ficha;
using SIGED_API.Models;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using REPORTE = SIGED_API.Models.REPORTE;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/reportedetalle")]
    [ApiController]
   // [Authorize]
    public class ReporteDetalleController : ControllerBase
    {

        private readonly AppDbContext2 context2;
        private readonly IWebHostEnvironment webHostEnviroment;

        public ReporteDetalleController(AppDbContext2 context2, IWebHostEnvironment webHost)
        {
            
            this.context2 = context2;
            
            webHostEnviroment = webHost;
        }
        // GET: api/<ReporteDetalleController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ReporteDetalleController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ReporteDetalleController>

        [HttpPost]
        public ActionResult Post([FromBody] ReporteRequest reportes)
        {
            decimal rutina1 = 0;
            decimal rutina2 = 0;
            decimal rutina3 = 0;
            decimal rutina4 = 0;
            decimal total = 0;
            var result = new OkObjectResult(0);
            try
            {
                
                foreach (var oPostrutina1 in reportes.rutinas1)
                {
                    RUTINA1 orutina1 = new RUTINA1();
                    orutina1.REPORTE_ID = reportes.reporte_id;
                    orutina1.RUTINA_ID = oPostrutina1.rutina_id;
                    orutina1.CALIFICACION = oPostrutina1.calificacion;
                    rutina1 +=  oPostrutina1.calificacion;
                    context2.RUTINA1.Add(orutina1);
                    context2.SaveChanges();
                }

               
                foreach (var oPostrutina2 in reportes.rutinas2)
                {
                    RUTINA2 orutina2 = new RUTINA2();
                    orutina2.REPORTE_ID = reportes.reporte_id;
                    orutina2.RUTINA_ID = oPostrutina2.rutina_id;
                    orutina2.CALIFICACION = oPostrutina2.calificacion;
                    rutina2 += oPostrutina2.calificacion;
                    context2.RUTINA2.Add(orutina2);
                    context2.SaveChanges();
                }

                foreach (var oPostrutina3 in reportes.rutinas3)
                {
                    RUTINA3 orutina3 = new RUTINA3();
                    orutina3.REPORTE_ID = reportes.reporte_id;
                    orutina3.RUTINA_ID = oPostrutina3.rutina_id;
                    orutina3.CALIFICACION = oPostrutina3.calificacion;
                    rutina3 += oPostrutina3.calificacion;
                    context2.RUTINA3.Add(orutina3);
                    context2.SaveChanges();
                }


                foreach (var oPostrutina4 in reportes.rutinas4)
                {
                    RUTINA4 orutina4 = new RUTINA4();
                    orutina4.REPORTE_ID = reportes.reporte_id;
                    orutina4.RUTINA_ID = oPostrutina4.rutina_id;
                    orutina4.CALIFICACION = oPostrutina4.calificacion;
                    rutina4 += oPostrutina4.calificacion;
                    context2.RUTINA4.Add(orutina4);
                    context2.SaveChanges();
                }
                
                //var vreporte = context2.REPORTE.FirstOrDefault(p => p.REPORTE_ID == reportes.reporte_id);

                var temporal_imagen = context2.TEMPORAL_IMAGEN.FirstOrDefault(p => p.TIPOARCHIVO == 1 & p.MODULO == 4);

                //if (vreporte != null )

                //{

                if (temporal_imagen != null)
                {
                
                    context2.TEMPORAL_IMAGEN.Remove(temporal_imagen);
                    context2.SaveChanges();

                }

                // REPORTE oreporte = new REPORTE();
                // oreporte.REPORTE_ID = reportes.reporte_id;
                    var oreporte = context2.REPORTE.FirstOrDefault(p => p.REPORTE_ID == reportes.reporte_id);
                    oreporte.POSTULANTE_ID = reportes.postulante_id;
                    oreporte.EVALUADOR_ID = reportes.evaluador_id;
                    oreporte.AREA_ID = reportes.evaluador_id;
                    oreporte.FECHA = reportes.fecha;
                    oreporte.CAL_RUTINA1 = rutina1;
                    oreporte.CAL_RUTINA2 = rutina2;
                    oreporte.CAL_RUTINA3 = rutina3;
                    oreporte.CAL_RUTINA4 = rutina4;
                    oreporte.NOTA_FINAL = rutina1+ rutina2+ rutina3+ rutina4;
                    oreporte.OBSERVACIONES = reportes.observaciones;
                  //  oreporte.ARCHIVO = temporal_imagen.archivo;
                    
                    oreporte.ESTADO = oreporte.NOTA_FINAL>18? "1":"0";
                    context2.Entry(oreporte).State = EntityState.Modified;
                    context2.SaveChanges();

                //}
                result = new OkObjectResult(new { message = "OK", status = true, reporte_id = reportes.reporte_id });

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



       [HttpPost("AdjuntarImagen/{id}")]
        public ActionResult PostImagen([FromForm] TemporalRequest temporal, int id)
        {
            try
            {
                TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                string uniqueFileName = UploadedImageReporte(temporal);
                opostulante.ARCHIVO = uniqueFileName;
                opostulante.DESCRIPCION = uniqueFileName;
                opostulante.TIPOARCHIVO = 1;
                opostulante.MODULO = 4;
                context2.TEMPORAL_IMAGEN.Add(opostulante);
                context2.SaveChanges();

                var opreporte = context2.REPORTE.FirstOrDefault(p => p.REPORTE_ID == id);
                if (opreporte != null)
                {
                    opreporte.ARCHIVO = uniqueFileName;
                    context2.Entry(opreporte).State = EntityState.Modified;
                    context2.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        private  String UploadedImageReporte(TemporalRequest temporal)
        {
            
            string uniqueFileName = null;
            if (temporal.Imagen != null)
            {
                string directorioActual = Directory.GetCurrentDirectory();
                var rutaGuardadoImagen = directorioActual + "/images/reporte";
                if (!Directory.Exists(rutaGuardadoImagen))
                {
                    Directory.CreateDirectory(rutaGuardadoImagen);
                }


                

                var nombreImag = Path.GetFileName(temporal.Imagen.FileName);
                var nombreImagen = "img_" + Guid.NewGuid().ToString("N") + "" + Path.GetExtension(nombreImag);
                var rutaImagen = Path.Combine(rutaGuardadoImagen, nombreImagen);


                using (var stream = new FileStream(rutaImagen, FileMode.Create))
                {
                     temporal.Imagen.CopyToAsync(stream);
                }

                /*     string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images/reporte");

                     uniqueFileName = temporal.Imagen.FileName;
                     string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                     using (var fileStream = new FileStream(filePath, FileMode.Create))
                     {
                         temporal.Archivo.CopyTo(fileStream);
                     }*/
                return nombreImagen;
            }
            
            return "error";
        }


        [HttpGet("GetImageReporte/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {

            var reporte = context2.REPORTE.FirstOrDefault(p => p.REPORTE_ID == id);

            string path = webHostEnviroment.ContentRootPath + "\\images\\reporte\\";
            var filePath = path + reporte.ARCHIVO;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            return null;
        }




        // PUT api/<ReporteDetalleController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReporteDetalleController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
