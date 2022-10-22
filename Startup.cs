using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SIGED_API.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGED_API
{
    public class Startup
    {
        private readonly string _MyCors = "";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddCors();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
            services.AddDbContext<AppDbContext2>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SIGED_API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyCors, builder =>
                {
                    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                 
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseCors(options =>
            //{
            //    options.WithOrigins("http://intranet.adexperu.edu.pe/APIS");
            //    options.WithOrigins("http://intranet.adexperu.edu.pe");
            //    options.AllowAnyMethod();
            //    options.AllowAnyHeader();
            //}
            //);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
     
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "SIGED_API v1"));
            //string virDir = Configuration.GetSection("VirtualDirectory").Value;
            //app.UseSwaggerUI(c =>
            //{
            //c.SwaggerEndpoint( virDir +"/swagger/v1/swagger.json", "SIGED_API v1");
            //});

            app.UseRouting();

            app.UseCors(_MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
