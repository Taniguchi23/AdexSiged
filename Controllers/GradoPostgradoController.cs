﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/postgrado")]
    [ApiController]
    [Authorize]
    public class GradoPostgradoController : ControllerBase
    {
        private readonly AppDbContext context;

        public GradoPostgradoController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<GradoPostgradoController>
        [HttpGet]
        public IEnumerable<GRADOACAD_POSTGRADO> Get()
        {

            return context.GRADOACAD_POSTGRADO.ToList();

        }

        // GET api/<GradoPostgradoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<GradoPostgradoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<GradoPostgradoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<GradoPostgradoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
