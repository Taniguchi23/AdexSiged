using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models.Common;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;
using SIGED_API.Tools;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SIGED_API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly AppSettings _appSettings;

        public UserService(AppDbContext context ,  IOptions<AppSettings> appSettings)
        {
            this.context = context;
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(Login  login)
        {

            UserResponse userresponse =  new UserResponse();
            string scontrasena = Encrypt.GetSHA256(login.contrasena);
            var usuario = context.Login.Where(d => d.correo == login.correo && d.contrasena == scontrasena).FirstOrDefault();

                if (usuario == null) return null;

                    userresponse.usuario = usuario.correo;
                    userresponse.token = GetToken(usuario);
                    userresponse.rol_id = usuario.rol_id;
                    userresponse.Rol = "Postulante";
                    userresponse.mensaje = "Usuario Correcto";
                    userresponse.nombre = usuario.nombre + " " + usuario.ape_paterno + " " + usuario.ape_materno;
                    userresponse.postulante_id = usuario.postulante_id;

            return userresponse;

            
        }


        public UserResponse Authadmin(Login login)
        {

            UserResponse userresponse = new UserResponse();

            //using (var db = new AppDbContext())
            //{
            //string scontrasena = Encrypt.GetSHA256(model.constrasena);

          

            //userresponse.usuario = usuario.correo;
            userresponse.token = GetTokenAdmin(login);
            //userresponse.rol_id = usuario.rol_id;
            //userresponse.Rol = "Postulante";
            //userresponse.mensaje = "Usuario Correcto";
            //userresponse.nombre = usuario.nombre + " " + usuario.ape_paterno + " " + usuario.ape_materno;
            //userresponse.postulante_id = usuario.postulante_id;
            //usuario.postulante_id





            //}

            return userresponse;


        }
        private string GetToken(Postulante login)
        {
            var toknHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, login.postulante_id.ToString()),
                        new Claim(ClaimTypes.Email, login.correo)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = toknHandler.CreateToken(tokenDescriptor);

            return toknHandler.WriteToken(token);


        }

        private string GetTokenAdmin(Login login)
        {
            var toknHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        //new Claim(ClaimTypes.NameIdentifier, login.postulante_id.ToString()),
                        new Claim(ClaimTypes.Email, login.correo)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = toknHandler.CreateToken(tokenDescriptor);

            return toknHandler.WriteToken(token);


        }
    }
}
