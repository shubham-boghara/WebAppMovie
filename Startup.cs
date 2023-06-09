using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.ApiRepository.cs;
using WebAppMovie.Data;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using WebAppMovie.Auth;
using WebAppMovie.ApiResponse;

namespace WebAppMovie
{
    public class Startup
    {
        public IConfiguration _configuration { get;  }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Database service
            services.AddDbContext<MovieDbContext>(opt => opt.UseSqlServer(_configuration.GetConnectionString("DefaultSqlConnection")));
           
            //Repositroy services
            services.AddScoped<IMovieAsyncAPIRepo, MovieAPIRepo>();
            services.AddScoped<IUserAsyncAPIRepo, UserAPIRepo>();

            //Auth repository services
            services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
            services.AddSingleton<ICustomUserManager, CustomUserManager>();

            //Responce repository services
            services.AddScoped<IAppResponse, AppResponse>();

            //Email repository services
            services.AddScoped<ICustomEmailManager, CustomEmailManager>();

            //services.AddSwaggerDocument();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new
                CamelCasePropertyNamesContractResolver();
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Movie API V1");
                
            });

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
