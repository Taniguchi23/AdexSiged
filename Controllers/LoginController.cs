using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;
using SIGED_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly AppDbContext context;

        private IUserService _userService;

        //public LoginController(AppDbContext context)
        //{
        //    this.context = context;
        //}

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<LoginController>/5
        //[HttpGet("{correo}/{contrasena}")]
        //public IEnumerable<Postulante> GetAll(string correo, string contrasena)
        //{
        //    return context.Postulante.Where(p => p.correo == correo && p.contrasena == contrasena);
  
        //}

        //[HttpGet("{id}")]
        //public Postulante Get(int id)
        //{

        //    var postulante = context.Postulante.FirstOrDefault(p => p.postulante_id == id);
        //    return postulante;
        //}


        // POST api/<LoginController>
        //[HttpPost]
        //public int Login(Login login)
        //{
            
        //        var post = context.Login.Where(p => p.correo == login.correo && p.contrasena == login.contrasena).FirstOrDefault();

        //        if (post != null)
        //        {
        //        return post.postulante_id;
        //        }
        //        else 
        //        {
        //        return 0;
        //        }
            
            
        //}

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] Login login)
        {

            Respuesta respuesta = new Respuesta();

            var userresponse = _userService.Auth(login);


            if (userresponse == null)
            {

                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o constrasena incorrecta";
                return BadRequest(respuesta);
            }

            respuesta.Exito = 1;

            respuesta.Data = userresponse;

            return Ok(respuesta);




        }


        // PUT api/<LoginController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<LoginController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
