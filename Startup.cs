using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SIGED_API.Contexts;
using SIGED_API.Models.Common;
using SIGED_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            services.AddDbContext<AppDbContext3>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));

            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddDbContext<AppDbContext>(a =>
            {
                a.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings"));
                a.EnableSensitiveDataLogging();
            });

            services.AddDbContext<AppDbContext2>(a =>
            {
                a.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings"));
                a.EnableSensitiveDataLogging();
            });

            services.AddDbContext<AppDbContext3>(a =>
            {
                a.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings"));
                a.EnableSensitiveDataLogging();
            });
            // jwt
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);

            services.AddAuthentication(d =>

            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(d =>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new TokenValidationParameters

                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(llave),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });

            services.AddScoped<IUserService, UserService>();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

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

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddAuthorization();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
