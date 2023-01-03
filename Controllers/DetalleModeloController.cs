using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modelo = SIGED_API.Models.Modelo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DetalleModeloController : ControllerBase
    {
        private readonly AppDbContext2 context2;
        private readonly IWebHostEnvironment webHostEnviroment;
   
        public DetalleModeloController(AppDbContext2 context2, IWebHostEnvironment webHost)
        {
            this.context2 = context2;
            webHostEnviroment = webHost;
        }
        // GET: api/<DetalleModeloController>
        [HttpGet]
        public ActionResult GETDETALLE([FromBody] ModeloDetalleRequest modelo)
        {
            try
            {

                     return Ok("Success");

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // GET api/<DetalleModeloController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<DetalleModeloController>
        [HttpPost]
        public ActionResult Post([FromBody] ModeloDetalleRequest modelos)
        {

            try
            {

                var temporal_imagen = context2.TEMPORAL_IMAGEN.FirstOrDefault(p => p.tipoarchivo == 1 & p.modulo == 3);

                    Modelo ommodelo = new Modelo();
                    
                    ommodelo.fecha_mod = modelos.fecha_mod;
                    ommodelo.observador_id = modelos.observador_id;
                    ommodelo.postulante_id = modelos.postulante_id;
                    ommodelo.Hora_inicial = modelos.Hora_inicial;
                    ommodelo.Hora_final = modelos.Hora_final;
                    ommodelo.area_id = modelos.area_id;
                    ommodelo.tema = modelos.tema;
                   
                if (temporal_imagen != null)
                {

                    ommodelo.referencia = temporal_imagen.archivo;
                    context2.TEMPORAL_IMAGEN.Remove(temporal_imagen);
                    context2.SaveChanges();

                }


                if (modelos.modelo_id != 0)
                {
                    ommodelo.modelo_id = modelos.modelo_id;
                    ommodelo.referencia = temporal_imagen.archivo;
                    ommodelo.max_puntaje = modelos.max_puntaje;
                    ommodelo.apreciacion = modelos.apreciacion;
                    context2.Entry(ommodelo).State = EntityState.Modified;
                    context2.SaveChanges();

                }
                else
                {
                    context2.Modelo.Add(ommodelo);
                    context2.SaveChanges();

                }

                foreach (var oPostrubricas in modelos.Rubricas_Modelo)
                {
                    Rubrica_Modelo orubricamodelo = new Rubrica_Modelo();
                    orubricamodelo.rubrica_id = oPostrubricas.rubrica_id;
                    orubricamodelo.modelo_id = modelos.modelo_id;
                    orubricamodelo.puntaje = oPostrubricas.puntaje;
                    context2.Rubrica_Modelo.Add(orubricamodelo);
                    context2.SaveChanges();
                }

                foreach (var oPosfortalezas in modelos.Fortalezas)
                {
                    Fortaleza ofortaleza = new Fortaleza();
                    ofortaleza.modelo_id = modelos.modelo_id;
                    ofortaleza.descripcion = oPosfortalezas.descripcion;
                    context2.Fortaleza.Add(ofortaleza);
                    context2.SaveChanges();
                }

                foreach (var oPosoportunidades in modelos.Oportunidades)
                {
                    Oportunidad ooportunidades = new Oportunidad();
                    ooportunidades.modelo_id = modelos.modelo_id;
                    ooportunidades.descripcion = oPosoportunidades.descripcion;
                    context2.Oportunidad.Add(ooportunidades);
                    context2.SaveChanges();
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // PUT api/<DetalleModeloController>/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id, [FromForm] Modelo modelo)
        //{
        //    try
        //    {


             
        //        if (modelo.modelo_id == id)
        //        {

        //        Modelo ommodelo = new Modelo();
        //        ommodelo.fecha_mod = modelo.fecha_mod;
        //        ommodelo.observador_id = modelo.observador_id;
        //        ommodelo.postulante_id = modelo.postulante_id;
        //        ommodelo.Hora_inicial = modelo.Hora_inicial;
        //        ommodelo.Hora_final = modelo.Hora_final;
        //        ommodelo.area_id = modelo.area_id;
        //        ommodelo.tema = modelo.tema;
        //            ommodelo.referencia = modelo.referencia;
        //            ommodelo.apreciacion = modelo.apreciacion;
         
        //        ommodelo.FrontImage = modelo.FrontImage;
        //        ommodelo.max_puntaje = modelo.max_puntaje;
        //        string uniqueFileNameimage = UploadedFileImage(ommodelo);
        //        ommodelo.referencia = uniqueFileNameimage;
        //        context2.Entry(ommodelo).State = EntityState.Modified;
        //        context2.SaveChanges();
        //            //}
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }

        //        return Ok("Success");

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok("Failed");
        //    }
        //}


        [HttpPost("AdjuntarImagen/{id}")]
        public ActionResult PostImagen([FromForm] TemporalRequest temporal)
        {
            try
            {
                TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                string uniqueFileName = UploadedFileModelo(temporal);
                opostulante.archivo = uniqueFileName;
                opostulante.descripcion = uniqueFileName;
                opostulante.tipoarchivo = 1;
                opostulante.modulo = 3;
                context2.TEMPORAL_IMAGEN.Add(opostulante);
                context2.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<DetalleModeloController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        private string UploadedFileModelo(TemporalRequest temporal)
        {
            string uniqueFileName = null;
            if (temporal.FrontArchivo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images");
                uniqueFileName = temporal.FrontArchivo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    temporal.FrontArchivo.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
    }
}
