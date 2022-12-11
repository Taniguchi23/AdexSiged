using Microsoft.AspNetCore.Mvc;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {

        private readonly AppDbContext2 context;

        public NotificacionController(AppDbContext2 context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<NotificacionController>
        [HttpGet]
        public IEnumerable<NOTIFICACION> Get()
        {

            return context.NOTIFICACION.ToList();

        }


        // GET api/<NotificacionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<NotificacionController>
        [HttpPost]
        public int GrabarReporte([FromBody] NOTIFICACION notificacion)
        {

            var vnotificacion = context.NOTIFICACION.FirstOrDefault(p => p.postulante_id == notificacion.postulante_id);

            if (vnotificacion != null)
            {
                return vnotificacion.notificacion_id;
            }
            else
            {
                NOTIFICACION oreporte = new NOTIFICACION();
                oreporte.fecha = notificacion.fecha;
                oreporte.postulante_id = notificacion.postulante_id;
                oreporte.fecha = notificacion.fecha;
                oreporte.estado = notificacion.estado;
                context.NOTIFICACION.Add(oreporte);
                context.SaveChanges();

                return oreporte.notificacion_id;
            }


        }


        [HttpPost("EnviarNotificacion/{id}")]
        public string Notificar([FromBody] NOTIFICACION notificacion)
        {

            return "value";
        }

        // PUT api/<NotificacionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<NotificacionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
