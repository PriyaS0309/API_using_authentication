using API_using_authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_using_authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DepartmentDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddAuthentication(
              options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }

              ).AddJwtBearer(o =>
              {
                  var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                  o.SaveToken = true;
                  o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["JWT:Issuer"],
                      ValidAudience = Configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(key),

                      ValidateLifetime = true,
                      ValidateIssuer = false,
                      ValidateAudience = false

                  };
              }

              );

            services.AddSwaggerGen(c=>                                                    //adding swagger
            {
                c.SwaggerDoc("v5", new OpenApiInfo
                { 
                   Title="API CRUD",
                   Version="5.6",
                   Description="REST API CRUD"
                });
            }
            );

            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IJWTManagerRepo, JWTMzanagerRepo>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();                                           //adding swagger in app
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v5/swagger.json", "5.6");
            });
        }
    }
}
