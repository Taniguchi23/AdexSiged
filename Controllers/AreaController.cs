using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreaController : ControllerBase
    {
        private readonly AppDbContext context;
        
        public AreaController(AppDbContext context)
        //public PostulanteController(AppDbContext context)
        {
            this.context = context;
        }


        // GET: api/<AreaController>
        [HttpGet]
        public IEnumerable<Area> Get()
        {
            
                return context.Area.ToList();
          
        }
    

        // GET api/<AreaController>/5
        [HttpGet("{id}")]
        public Area Get(int id)
        {
            var area = context.Area.FirstOrDefault(p => p.area_id == id);
            return area;
        }

        // POST api/<AreaController>
        //[HttpPost]
        //public ActionResult Post([FromBody] Area area)
        //{
        //    try
        //    {
        //        context.Area.Add(area);
        //        context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        // PUT api/<AreaController>/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id, [FromBody] Area area)
        //{
        //    if (area.area_id == id)
        //    {
        //        context.Entry(area).State = EntityState.Modified;
        //        context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        // DELETE api/<AreaController>/5
        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    var area = context.Area.FirstOrDefault(p => p.area_id == id);
        //    if (area != null)
        //    {
        //        context.Area.Remove(area);
        //        context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
