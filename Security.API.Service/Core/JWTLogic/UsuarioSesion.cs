using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Core.JWTLogic
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _ihttpContextAccesor;


        public UsuarioSesion(IHttpContextAccessor ihttpContextAccesor)
        {
            _ihttpContextAccesor = ihttpContextAccesor;
        }
        public string GetUserSession(string jwtoken)
        {
            var userNameTest = _ihttpContextAccesor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
            //var userName = _ihttpContextAccesor.HttpContext;

            var userName = this.DecodeJwtoken(jwtoken);

            return userName;
        }

        public string DecodeJwtoken(string jwtoken)
        {
            
            var handle = new JwtSecurityTokenHandler();
            var jsonToken = handle.ReadJwtToken(jwtoken);
            var username = jsonToken.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;

            return username;
        }
    }
}
