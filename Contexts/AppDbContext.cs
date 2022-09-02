using Microsoft.EntityFrameworkCore;
using SIGED_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API.Contexts
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Postulante> Postulante { get; set; }
}
}
