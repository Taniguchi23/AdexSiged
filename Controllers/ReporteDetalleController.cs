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

            try
            {
                
                foreach (var oPostrutina1 in reportes.rutinasone)
                {
                    RUTINA1 orutina1 = new RUTINA1();
                    orutina1.REPORTE_ID = reportes.reporte_id;
                    orutina1.RUTINA_ID = oPostrutina1.rutina_id;
                    orutina1.CALIFICACION = oPostrutina1.calificacion;
                    context2.RUTINA1.Add(orutina1);
                    context2.SaveChanges();
                }

               
                foreach (var oPostrutina2 in reportes.rutinastwo)
                {
                    RUTINA2 orutina2 = new RUTINA2();
                    orutina2.REPORTE_ID = reportes.reporte_id;
                    orutina2.RUTINA_ID = oPostrutina2.rutina_id;
                    orutina2.CALIFICACION = oPostrutina2.calificacion;
                    context2.RUTINA2.Add(orutina2);
                    context2.SaveChanges();
                }

                foreach (var oPostrutina3 in reportes.rutinastree)
                {
                    RUTINA3 orutina3 = new RUTINA3();
                    orutina3.REPORTE_ID = reportes.reporte_id;
                    orutina3.RUTINA_ID = oPostrutina3.rutina_id;
                    orutina3.CALIFICACION = oPostrutina3.calificacion;
                    context2.RUTINA3.Add(orutina3);
                    context2.SaveChanges();
                }


                foreach (var oPostrutina4 in reportes.rutinasfour)
                {
                    RUTINA4 orutina4 = new RUTINA4();
                    orutina4.REPORTE_ID = reportes.reporte_id;
                    orutina4.RUTINA_ID = oPostrutina4.rutina_id;
                    orutina4.CALIFICACION = oPostrutina4.calificacion;
                    context2.RUTINA4.Add(orutina4);
                    context2.SaveChanges();
                }

                //Modelo ommodelo = new Modelo();
                //ommodelo.modelo_id = modelos.modelo_id;
                //ommodelo.fecha_mod = modelos.fecha_mod;
                //ommodelo.observador_id = modelos.observador_id;
                //ommodelo.postulante_id = modelos.postulante_id;
                //ommodelo.Hora_inicial = modelos.Hora_inicial;
                //ommodelo.Hora_final = modelos.Hora_final;
                //ommodelo.area_id = modelos.area_id;
                //ommodelo.tema = modelos.tema;

                //ommodelo.apreciacion = modelos.apreciacion;
                //ommodelo.FrontImage = modelos.FrontImage;
                //ommodelo.max_puntaje = modelos.max_puntaje;
                //if (ommodelo.FrontImage != null)
                //{
                //    string uniqueFileNameimage = UploadedFileImage(ommodelo);
                //    ommodelo.referencia = uniqueFileNameimage;
                //}
                //context2.Entry(ommodelo).State = EntityState.Modified;
                //context2.SaveChanges();
                return Ok("Success");

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
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
