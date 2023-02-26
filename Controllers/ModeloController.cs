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
using Microsoft.EntityFrameworkCore;
using Modelo = SIGED_API.Entity.Modelo;
using Microsoft.AspNetCore.Authorization;
//using Modelo2 = SIGED_API.Models.Modelo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/modelo")]
    [ApiController]
    [Authorize]
    public class ModeloController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly AppDbContext2 context2;
        private readonly IWebHostEnvironment webHostEnviroment;
        public ModeloController(AppDbContext context, AppDbContext2 context2, IWebHostEnvironment webHost)
        {
            this.context = context;
            this.context2 = context2;
            webHostEnviroment = webHost;
        }
        // GET: api/<ModeloController>
        [HttpGet]
        public IEnumerable<Modelo> Get()
        {

            return context.Modelo.ToList();

        }

        // GET api/<ModeloController>/5
        [HttpGet("{id}")]
        public Modelo Get(int id)
        {
            var modelo = context.Modelo.FirstOrDefault(p => p.modelo_id == id);
            return modelo;
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
                    //ommodelo.referencia = null;
                    //ommodelo.apreciacion = modelo.apreciacion;
                    //ommodelo.max_puntaje = modelo.max_puntaje;
                    context.Modelo.Add(ommodelo);
                    context.SaveChanges();

                    return ommodelo.modelo_id;
                }
            
            
        }

        // PUT api/<ModeloController>/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id, [FromForm] Modelo modelo)
        //{
        //    if (modelo.modelo_id == id)
        //    {
                
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //}

        //// DELETE api/<ModeloController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

     
    }
}
