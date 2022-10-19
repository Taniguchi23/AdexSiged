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
using System.Linq;

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
        public int GrabarModelo([FromBody] Modelo modelo)
        {
            
                var vmodelo = context.Modelo.FirstOrDefault(p => p.postulante_id == modelo.postulante_id &  p.area_id == modelo.area_id);

                if (vmodelo != null)
                {
                    return vmodelo.modelo_id;
                }
                else
                {
                    Modelo ommodelo = new Modelo();
                    ommodelo.fecha_mod = modelo.fecha_mod;
                    ommodelo.observador_id = modelo.observador_id;
                    ommodelo.postulante_id = modelo.postulante_id;
                    ommodelo.Hora_inicial = modelo.Hora_inicial;
                    ommodelo.Hora_final = modelo.Hora_final;
                    ommodelo.area_id = modelo.area_id;
                    ommodelo.tema = modelo.tema;
                    //omodelo.apreciacion = modelo.apreciacion;
                    //omodelo.referencia = modelo.referencia;
                    //omodelo.FrontImage = modelo.FrontImage;
                    //string uniqueFileNameimage = UploadedFileImage(omodelo);
                    //omodelo.referencia = uniqueFileNameimage;
                    context.Modelo.Add(ommodelo);
                    context.SaveChanges();

                    return modelo.modelo_id;
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
