using Microsoft.EntityFrameworkCore;
using SIGED_API.Entity;
//using SIGED_API.Entity;
//using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postulante = SIGED_API.Models.Request.Postulante;

namespace SIGED_API.Contexts
{
    public class AppDbContext4:DbContext
    {

        public AppDbContext4(DbContextOptions<AppDbContext4> options):base(options)
        {
        }

        public DbSet<Postulante> Postulante { get; set; }
       

    }
}
