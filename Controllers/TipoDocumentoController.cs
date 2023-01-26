using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly AppDbContext context;


        private readonly IWebHostEnvironment webHostEnviroment;
        public TipoDocumentoController(AppDbContext context,  IWebHostEnvironment webHost)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;

            webHostEnviroment = webHost;
        }


        // GET: api/<TipoDocumentoController>
        [HttpGet]
        public IEnumerable<TIPO_DOCUMENTO> Get()
        {
            try
            {
                return context.TIPO_DOCUMENTO.ToList().Where((c => c.estado == true));
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        // GET api/<TipoDocumentoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<TipoDocumentoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TipoDocumentoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TipoDocumentoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
