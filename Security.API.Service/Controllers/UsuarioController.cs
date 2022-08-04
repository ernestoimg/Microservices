using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.API.Service.Core.Application;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IMediator _imediator;

        public UsuarioController(IMediator imediator)
        {
            _imediator = imediator;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(Register.UsuarioRegisterCommand param)
        {
            return await _imediator.Send(param);
        } 

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(Login.UsuarioLoginCommand param)
        {
            return await _imediator.Send(param);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> Get([FromQuery]UsuarioActual.UsuarioActualCommand param)
        {
            return await _imediator.Send(param);
        }
    }
}
