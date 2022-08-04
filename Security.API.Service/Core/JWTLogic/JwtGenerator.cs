using Microsoft.IdentityModel.Tokens;
using Security.Configurations;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.API.Service.Core.JWTLogic
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IAppSettings _appSettings;

        public JwtGenerator(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.NameId,usuario.UserName)
                new Claim("username",usuario.UserName),
                new Claim("nombre", usuario.Nombre),
                new Claim("apellido",usuario.Apellido)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.GetSecretWord()));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential,   
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
