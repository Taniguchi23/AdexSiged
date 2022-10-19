using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
    public class DetalleModeloController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public DetalleModeloController(AppDbContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            webHostEnviroment = webHost;
        }
        // GET: api/<DetalleModeloController>
        [HttpGet]
        public ActionResult Post([FromBody] ModeloRequest modelo)
        {
            try
            {
                

                List<Rubrica_Modelo> rubricas = JsonConvert.DeserializeObject<List<Rubrica_Modelo>>(modelo.Rubricas_Modelo);
                foreach (var opostrubricas in rubricas)
                {
                    Rubrica_Modelo orubricamodelo = new Rubrica_Modelo();
                    orubricamodelo.rubrica_id = opostrubricas.rubrica_id;
                    orubricamodelo.modelo_id = modelo.modelo_id;
                    orubricamodelo.puntaje = opostrubricas.puntaje;
                    context.Rubrica_Modelo.Add(orubricamodelo);
                    context.SaveChanges();
                }


                List<Fortaleza> fortalezas = JsonConvert.DeserializeObject<List<Fortaleza>>(modelo.Fortalezas);
                foreach (var opostfortalezas in fortalezas)
                {
                    Fortaleza ofortaleza = new Fortaleza();
                    ofortaleza.modelo_id = modelo.modelo_id;
                    ofortaleza.descripcion = opostfortalezas.descripcion;
                    context.Fortaleza.Add(ofortaleza);
                    context.SaveChanges();
                }

                List<Oportunidad> oportunidades = JsonConvert.DeserializeObject<List<Oportunidad>>(modelo.Oportunidades);
                foreach (var opostfortalezas in fortalezas)
                {
                    Fortaleza ofortaleza = new Fortaleza();
                    ofortaleza.modelo_id = modelo.modelo_id;
                    ofortaleza.descripcion = opostfortalezas.descripcion;
                    context.Fortaleza.Add(ofortaleza);
                    context.SaveChanges();
                }


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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DetalleModeloController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DetalleModeloController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
