﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/evaluaciontecnica")]
    [ApiController]
    [Authorize]
    public class EvaluacionTecnicaController : ControllerBase
    {
        private readonly AppDbContext context;
        public EvaluacionTecnicaController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<EvaluacionTecnicaController>
        [HttpGet]
        public IEnumerable<E_Tecnica> Get()
        {
            return context.E_Tecnica.ToList();
        }

        // GET api/<EvaluacionTecnicaController>/5
        [HttpGet("{id}")]
        public E_Tecnica Get(int id)
        {
            var tecnica = context.E_Tecnica.FirstOrDefault(p => p.e_tecnica_id == id);
            return tecnica;
        }

        // POST api/<EvaluacionTecnicaController>
        [HttpPost]
        public ActionResult Post([FromBody] EvaluacionTecnica_Request tecnica)
        {
            try
            {
                E_Tecnica otecnica = new E_Tecnica();
                otecnica.fecha = tecnica.fecha;
                otecnica.comentario_1 = tecnica.comentario_1;
                otecnica.comentario_2 = tecnica.comentario_2;
                otecnica.comentario_3 = tecnica.comentario_3;
                otecnica.comentario_4 = tecnica.comentario_4;
                otecnica.comentario_5 = tecnica.comentario_5;
                otecnica.comentario_6 = tecnica.comentario_6;
                otecnica.comentario_7 = tecnica.comentario_7;
                otecnica.comentario_8 = tecnica.comentario_8;
                otecnica.comentario_9 = tecnica.comentario_9;
                otecnica.comentario_10 = tecnica.comentario_10;
                otecnica.comentario_11 = tecnica.comentario_11;
                otecnica.apreciacion = tecnica.apreciacion;
                otecnica.id_hora_pedagogica = tecnica.id_hora_pedagogica;
                otecnica.observacion = tecnica.observacion;
                otecnica.estado = tecnica.estado;
                context.E_Tecnica.Add(otecnica);
                context.SaveChanges();

                Seleccion_detalle oseleccion_detalle = new Seleccion_detalle();
                oseleccion_detalle.seleccion_id = tecnica.seleccion_id;
                oseleccion_detalle.e_tecnica_id = otecnica.e_tecnica_id;
                context.Seleccion_detalle.Add(oseleccion_detalle);
                context.SaveChanges();
                var result = new OkObjectResult(new { message = "OK", status = true });
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // PUT api/<EvaluacionTecnicaController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EvaluacionTecnicaController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
