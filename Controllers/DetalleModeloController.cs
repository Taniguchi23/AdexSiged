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
using Modelo = SIGED_API.Models.Modelo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DetalleModeloController>
        [HttpPost]
        public ActionResult Post([FromForm] ModeloDetalleRequest modelos)
        {
            try
            {
                List<Rubrica_Modelo> rubricas = JsonConvert.DeserializeObject<List<Rubrica_Modelo>>(modelos.Rubricas_Modelo);
                foreach (var oPostrubricas in rubricas)
                {
                    Rubrica_Modelo orubricamodelo = new Rubrica_Modelo();
                    orubricamodelo.rubrica_id = oPostrubricas.rubrica_id;
                    orubricamodelo.modelo_id = modelos.modelo_id;
                    orubricamodelo.puntaje = oPostrubricas.puntaje;
                    context2.Rubrica_Modelo.Add(orubricamodelo);
                    context2.SaveChanges();
                }

                List<Fortaleza> fortalezas = JsonConvert.DeserializeObject<List<Fortaleza>>(modelos.Fortalezas);
                foreach (var oPosfortalezas in fortalezas)
                {
                    Fortaleza ofortaleza = new Fortaleza();
                    ofortaleza.modelo_id = modelos.modelo_id;
                    ofortaleza.descripcion = oPosfortalezas.descripcion;
                    context2.Fortaleza.Add(ofortaleza);
                    context2.SaveChanges();
                }

                List<Oportunidad> oportunidades = JsonConvert.DeserializeObject<List<Oportunidad>>(modelos.Oportunidades);
                foreach (var oPosoportunidades in oportunidades)
                {
                    Oportunidad ooportunidades = new Oportunidad();
                    ooportunidades.modelo_id = modelos.modelo_id;
                    ooportunidades.descripcion = oPosoportunidades.descripcion;
                    context2.Oportunidad.Add(ooportunidades);
                    context2.SaveChanges();
                }

                Modelo ommodelo = new Modelo();
                    ommodelo.modelo_id = modelos.modelo_id;
                    ommodelo.fecha_mod = modelos.fecha_mod;
                    ommodelo.observador_id = modelos.observador_id;
                    ommodelo.postulante_id = modelos.postulante_id;
                    ommodelo.Hora_inicial = modelos.Hora_inicial;
                    ommodelo.Hora_final = modelos.Hora_final;
                    ommodelo.area_id = modelos.area_id;
                    ommodelo.tema = modelos.tema;
                   
                    ommodelo.apreciacion = modelos.apreciacion;
                    ommodelo.FrontImage = modelos.FrontImage;
                    ommodelo.max_puntaje = modelos.max_puntaje;
                    if (ommodelo.FrontImage != null)
                    {
                    string uniqueFileNameimage = UploadedFileImage(ommodelo);
                    ommodelo.referencia = uniqueFileNameimage;
                    }
                    context2.Entry(ommodelo).State = EntityState.Modified;
                    context2.SaveChanges();
                    return Ok("Success");

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // PUT api/<DetalleModeloController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] Modelo modelo)
        {
            try
            {


             
                if (modelo.modelo_id == id)
                {

                Modelo ommodelo = new Modelo();
                ommodelo.fecha_mod = modelo.fecha_mod;
                ommodelo.observador_id = modelo.observador_id;
                ommodelo.postulante_id = modelo.postulante_id;
                ommodelo.Hora_inicial = modelo.Hora_inicial;
                ommodelo.Hora_final = modelo.Hora_final;
                ommodelo.area_id = modelo.area_id;
                ommodelo.tema = modelo.tema;
                    ommodelo.referencia = modelo.referencia;
                    ommodelo.apreciacion = modelo.apreciacion;
         
                ommodelo.FrontImage = modelo.FrontImage;
                ommodelo.max_puntaje = modelo.max_puntaje;
                string uniqueFileNameimage = UploadedFileImage(ommodelo);
                ommodelo.referencia = uniqueFileNameimage;
                context2.Entry(ommodelo).State = EntityState.Modified;
                context2.SaveChanges();
                    //}
                }
                else
                {
                    return BadRequest();
                }

                return Ok("Success");

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // DELETE api/<DetalleModeloController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private string UploadedFileImage(Modelo modelo)
        {
            string uniqueFileName = null;
            if (modelo.Hora_final != null)
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelo.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelo.FrontImage.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
    }
}
