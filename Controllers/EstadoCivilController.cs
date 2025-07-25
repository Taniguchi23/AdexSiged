﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/estadocivil")]
    [ApiController]
    [Authorize]
    public class EstadoCivilController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstadoCivilController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<EstadoCivilController>
        [HttpGet]
        public IEnumerable<ESTADOCIVIL> Get()
        {

            return context.ESTADOCIVIL.ToList();

        }


        // GET api/<EstadoCivilController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<EstadoCivilController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<EstadoCivilController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<EstadoCivilController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
