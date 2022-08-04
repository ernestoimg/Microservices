using Microsoft.AspNetCore.Identity;
using Security.API.Service;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Core.Context
{
    public class SeguridadData
    {
        public static async Task InsertarUsuario(SeguridadContexto context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var user = new Usuario()
                {
                    Nombre = "ivan",
                    Apellido = "martinez",
                    Direccion = "Bosques de Manzanilla",
                    UserName = "imartinez",
                    Email = "ivan2017city@gmail.com"
                };

                await usuarioManager.CreateAsync(user, "Pas$W0rd1");

            }
        }

    }
}
