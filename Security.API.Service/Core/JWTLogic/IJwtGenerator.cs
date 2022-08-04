using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Core.JWTLogic
{
    public interface IJwtGenerator
    {
        string CreateToken(Usuario usuario);
    }
}
