using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Services.Configurations;
using Services.ContextMongoDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Servicios.API.Libreria
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly string _MyCors = "CorsRule";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:DataBase").Value;
                }
                );
            services.AddSingleton<MongoSettings>();
            services.AddTransient<IAutorContext, AutorContext>();
            services.AddTransient<IAutorRepository,AutorRepository>();
            services.AddScoped(typeof(IMongoRepository<>),typeof(MongoRepository<>));


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Servicios.API.Libreria", Version = "v1" });
                // Set the comments path the swagger JSON and UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy(name:_MyCors, rule =>
                {
                    //rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                    rule.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicios.API.Libreria v1"));
            }

            app.UseCors(_MyCors);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

        }
    }
}
