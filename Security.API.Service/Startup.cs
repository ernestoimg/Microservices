using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Security.API.Service.Core.Context;
using Security.API.Service.Core.JWTLogic;
using Security.Entities;
using System;
using System.Text;

namespace Security.API.Service
{
    public class Startup
    {
        private readonly string _MyCors = "CorsRule";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SeguridadContexto>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ConexionDb"));
            });

            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Register>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Security.API.Service", Version = "v1" });
            });

            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<SeguridadContexto>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();

            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddAutoMapper(typeof(Register.UsuarioRegisterHandler));

            //mapper 
            var mapperConfig = new MapperConfiguration(m =>
             {
                 m.AddProfile(new MappingProfile());
             });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            //mapper

            services.AddMediatR(typeof(Register.UsuarioRegisterCommand).Assembly);

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddScoped<IAppSettings, AppSettings>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Key:SecretWord").Value.ToString()));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lGoxWe5RDGL3roOzoKt21aHtUFN6U1Zz"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            services.AddHttpContextAccessor();

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _MyCors, rule =>
                {
                    //rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                    rule.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Security.API.Service v1"));
            }

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
