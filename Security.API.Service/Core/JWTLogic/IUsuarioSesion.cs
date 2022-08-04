using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Core.JWTLogic
{
    public interface IUsuarioSesion
    {
        string GetUserSession(string jwt);
        string DecodeJwtoken(string jwtoken);
    }
}
