using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using HeroesApi.Services;
using HeroesApi.Models;

using Microsoft.Extensions.Options;
namespace HeroesApi
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


            services.AddCors(c =>  
            {  
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod() );
                                        //AllowAnyOrigin());  
                                        
            });

            // requires using Microsoft.Extensions.Options
            services.Configure<HeroesDatabaseSettings>(
                Configuration.GetSection(nameof(HeroesDatabaseSettings)));

            services.AddSingleton<IHeroesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<HeroesDatabaseSettings>>().Value);
            services.AddSingleton<HeroService>();

            services.AddControllers();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeroesApi v1");
                    c.RoutePrefix = string.Empty;
            });



            app.UseRouting();


            app.UseCors();
            //options => options.AllowAnyOrigin());  
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
