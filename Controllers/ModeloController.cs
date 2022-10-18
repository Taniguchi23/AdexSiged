using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public ModeloController(AppDbContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            webHostEnviroment = webHost;
        }
        // GET: api/<ModeloController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ModeloController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ModeloController>
        [HttpPost]
        public ActionResult Post([FromForm] Modelo modelo)
        {
            try
            {
                Modelo omodelo = new Modelo();
                omodelo.fecha_mod = modelo.fecha_mod;
                omodelo.observador_id = modelo.observador_id;
                omodelo.postulante_id = modelo.postulante_id;
                omodelo.Hora_inicial = modelo.Hora_inicial;
                omodelo.Hora_final = modelo.Hora_final;
                omodelo.area_id = modelo.area_id;
                omodelo.tema = modelo.tema;
                omodelo.apreciacion = modelo.apreciacion;
                omodelo.referencia = modelo.referencia;
                omodelo.FrontImage = modelo.FrontImage;
                string uniqueFileNameimage = UploadedFileImage(omodelo);
                omodelo.referencia = uniqueFileNameimage;
                context.Modelo.Add(omodelo);
                context.SaveChanges();


                //List<Especialidad_postulante> especialidadess = JsonConvert.DeserializeObject<List<Especialidad_postulante>>(postulante.Especialidades);
                //foreach (var oPostEspecialidad in especialidadess)
                //{
                //    Especialidad_postulante oespecialidad = new Especialidad_postulante();
                //    oespecialidad.postulante_id = opostulante.postulante_id;
                //    oespecialidad.especialidad_id = oPostEspecialidad.especialidad_id;
                //    context.Especialidad_postulante.Add(oespecialidad);
                //    context.SaveChanges();
                //}
                return Ok("Success");

            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }

        // PUT api/<ModeloController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModeloController>/5
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
