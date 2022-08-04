using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Core.JWTLogic
{
    public interface IAppSettings
    {
        string GetSecretWord();
    }
}
