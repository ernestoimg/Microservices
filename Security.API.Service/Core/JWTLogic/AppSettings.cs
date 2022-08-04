using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Core.JWTLogic
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSecretWord()
        {
            var secretWord = _configuration.GetValue<string>("Key:SecretWord");

            return secretWord;
        }
    }
}
