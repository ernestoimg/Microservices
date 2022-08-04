using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Security.API.Service.Core.Context;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var hostServer = CreateHostBuilder(args).Build();

            using (var context = hostServer.Services.CreateScope())
            {
                var services = context.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();

                    var _contextEF = services.GetRequiredService<SeguridadContexto>();

                    SeguridadData.InsertarUsuario(_contextEF, userManager).Wait();
                }
                catch (Exception e)
                {
                    var loggin = services.GetRequiredService<ILogger<Program>>();

                    loggin.LogError(e, "error cuando registra usuario");
                }
            }

            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
