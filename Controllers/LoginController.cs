using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;
using SIGED_API.Services;
using SIGED_API.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WinAuthentication.Authentication.Windows;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/login")]
    [ApiController]
    //[Authorize]
    public class LoginController : ControllerBase
    {

        private readonly AppDbContext context;

        private IUserService _userService;

        public string url = "http://10.31.1.37/ApiAutenticationAD/api/auth/login";

        //public LoginController(AppDbContext context)
        //{
        //    this.context = context;
        //}

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        //GET: api/<LoginController>
        [HttpGet("get")]
        public IEnumerable<string> Get()
        {
            string param = HttpContext.User.Identity.Name;
            return new string[] { param, param };
        }

        // GET api/<LoginController>/5
        //[HttpGet("{correo}/{contrasena}")]
        //public IEnumerable<Postulante> GetAll(string correo, string contrasena)
        //{
        //    return context.Postulante.Where(p => p.correo == correo && p.contrasena == contrasena);

        //}

        //[HttpGet("login/get")]
        //public string Get()
        //{
        //    string name = "Hola";

        //    return name;
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

        [HttpPost("")]
        public async Task<ActionResult> Autentificar([FromBody] Login login)
        {
            Respuesta respuesta = new Respuesta();
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var httpClient = new HttpClient())
            {
                //string scontrasena = Encrypt.GetSHA256(login.contrasena);
                var loginuser = new UserRequest() { UserName = login.correo, Password = login.contrasena };

                var respuestaa = await httpClient.PostAsJsonAsync(url, loginuser);
                var responseadmin = await respuestaa.Content.ReadFromJsonAsync<Roles>();
                


                if (responseadmin.status)
                {
                    var contentjs = await respuestaa.Content.ReadAsStringAsync();

                    var roles = System.Text.Json.JsonSerializer.Deserialize<Roles>(contentjs, jsonSerializerOptions);

                    UserResponse userrequest = new UserResponse();

                    var userresponseadmin = _userService.Authadmin(login);

                    userrequest.nombre = roles.fullname;
                    userrequest.usuario = login.correo;
                    userrequest.rol_id = roles.rol;
                    userrequest.Rol = roles.descripcion;
                    userrequest.mensaje = roles.message;
                    userrequest.token = userresponseadmin.token;

                    //if (roles.status is true)
                    //{

                        respuesta.status = true;
                        respuesta.Data = userrequest;
                        return Ok(respuesta);
                    //}
                }
                else
                    {
                        var userresponse = _userService.Auth(login);

                        if (userresponse == null)
                        {

                            //respuesta.Exito = 0;
                            //respuesta.Mensaje = "Usuario o constrasena incorrecta";
                            return BadRequest(respuesta);
                        }

                        respuesta.status = true;
                        //respuesta.message = "Usuario Correcto";
                        //respuesta.fullname = userresponse.Email;
                        //respuesta.rol = 3;
                        //respuesta.descripcion = "Postulante" ;
                        respuesta.Data = userresponse;

                        return Ok(respuesta);
                        
                    }

                    
                
                //else
                //{

                //   return BadRequest();

                    
                //}

            }


                ///////////////////////////////////////////

        

        




            //Respuesta respuesta = new Respuesta();

            //var userresponse = _userService.Auth(login);

            


            //if (userresponse == null)
            //{

            //    respuesta.Exito = 0;
            //    respuesta.Mensaje = "Usuario o constrasena incorrecta";
            //    return BadRequest(respuesta);
            //}

            //respuesta.Exito = 1;

            //respuesta.Data = userresponse;

            //return Ok(respuesta);




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
